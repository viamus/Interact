using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static Interact.Instance.Data.Postgresql.InteractDomain.Security.User;
using Interact.Instance.Data.Postgresql.InteractDomain.Security;
using Interact.Instance.Web.Api.Models.Account.Request;
using Microsoft.AspNetCore.Cors;
using Interact.Library;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Interact.Instance.Web.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ILogger _logger;

        private readonly TokenAuthOption _tokenOptions;

        private readonly TokenConfigurations _tokenConfigurations;

        private readonly SigningConfigurations _signingConfigurations;

        private readonly IDistributedCache _cache;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IOptions<TokenAuthOption> tokenOptions,
            TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations,
            IDistributedCache cache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _tokenOptions = tokenOptions.Value;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
            _cache = cache;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [HttpPost]
        public object Login([FromBody] AccessCredentials model)
        {
            bool isValid = false;

            if (model.GrantType == "password")
            {
                if (model.PasswordHash.NullOrEmpty() || model.UserName.NullOrEmpty())
                {
                    return new
                    {
                        authenticated = false,
                        message = "Login failed"
                    };
                }

                var userIdentity = _userManager.FindByNameAsync(model.UserName).Result;

                if (userIdentity != null)
                {
                    var response = _signInManager.CheckPasswordSignInAsync(userIdentity, model.PasswordHash, false).Result;

                    if (response.Succeeded)
                    {
                        isValid = _userManager.IsInRoleAsync(userIdentity, Roles.ROLE_API_ACCOUNT).Result;
                    }
                }
            }
            else if (model.GrantType == "refresh_token")
            {
                if (!String.IsNullOrWhiteSpace(model.RefreshToken))
                {
                    RefreshTokenData refreshTokenBase = null;

                    string strTokenArmazenado =
                        _cache.GetString(model.RefreshToken);

                    if (!string.IsNullOrWhiteSpace(strTokenArmazenado))
                    {
                        refreshTokenBase = JsonConvert
                            .DeserializeObject<RefreshTokenData>(strTokenArmazenado);
                    }

                    isValid = (refreshTokenBase != null &&
                        model.UserName == refreshTokenBase.UserName &&
                        model.RefreshToken == refreshTokenBase.RefreshToken);

                    // Elimina o token de refresh já que um novo será gerado
                    if (isValid)
                    {
                        _cache.Remove(model.RefreshToken);
                    }
                }
            }

            if (isValid)
            {
                return GenerateToken(
                    model.UserName, _signingConfigurations,
                    _tokenConfigurations, _cache);
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Login failed"
                };
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation(4, "User logged out.");

            return Ok();
        }

        private object GenerateToken(string userName,
             SigningConfigurations signingConfigurations,
             TokenConfigurations tokenConfigurations,
             IDistributedCache cache)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(userName, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userName)
                }
            );

            DateTime createdAt = DateTime.Now;
            DateTime expireIn = createdAt + TimeSpan.FromSeconds(180);

            // Calcula o tempo máximo de validade do refresh token
            // (o mesmo será invalidado automaticamente pelo Redis)
            TimeSpan cacheExpiration = TimeSpan.FromMinutes(60);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createdAt,
                Expires = expireIn
            });

            var token = handler.WriteToken(securityToken);

            var response = new
            {
                authenticated = true,
                userName = userName,
                //created = createdAt.ToString("yyyy-MM-dd HH:mm:ss"),
                created = createdAt.Ticks,
                accessTokenExpire = expireIn.ToUniversalTime().Ticks,
                refreshTokenExpire = (expireIn + cacheExpiration).ToUniversalTime().Ticks,
                //expiration = expireIn.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                refreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
                message = "OK"
            };

            // Armazena o refresh token em cache através do Redis 
            var refreshTokenData = new RefreshTokenData();
            refreshTokenData.RefreshToken = response.refreshToken;
            refreshTokenData.UserName = userName;

            DistributedCacheEntryOptions cacheOptions =
                new DistributedCacheEntryOptions();

            cacheOptions.SetAbsoluteExpiration(cacheExpiration);

            cache.SetString(response.refreshToken,
                JsonConvert.SerializeObject(refreshTokenData),
                cacheOptions);

            return response;
        }

        //private string GenerateToken(DateTime expires, ClaimsIdentity claims)
        //{
        //    var handler = new JwtSecurityTokenHandler();

        //    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        //    {
        //        Issuer = _tokenOptions.Issuer,
        //        Audience = _tokenOptions.Audience,
        //        SigningCredentials = _tokenOptions.SigningCredentials,
        //        Subject = claims,
        //        NotBefore = DateTime.Now,
        //        Expires = DateTime.Now.AddSeconds(_tokenConfigurations.Seconds),
        //    });

        //    return handler.WriteToken(securityToken);
        //}
    }
}
