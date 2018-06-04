using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Interact.Instance.Data.Postgresql.InteractDomain.Context;
using Interact.Instance.Data.Postgresql.InteractDomain.Security;
using Interact.Instance.Web.Api.Helpers;
using Interact.Instance.Web.Api.Models;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static Interact.Instance.Data.Postgresql.InteractDomain.Security.User;

namespace Interact.Instance.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static readonly log4net.ILog _Log = log4net.LogManager.GetLogger(typeof(Program));

        private static IConfiguration _configuration;

        private static ServiceProvider _services;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = _configuration.GetConnectionString("RedisConnectionString");
                option.InstanceName = "interact_api_redis";

            });

            LoadAppConfig();
            LoadServices(services);
            LoadCors(services);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IdentityContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("SiteCors");

            app.UseExceptionHandler(
            builder =>
            {
                builder.Run(async c =>
                {
                    c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    c.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                    var error = c.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        //c.Response.AddApplicationError(error.Error.Message);
                        await c.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                    }
                });
            });

            new IdentityInitializer(context, userManager, roleManager).Initialize();

            //app.UseAuthentication();
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseMvc();
        }

        private static void LoadLog4Net()
        {
            XmlDocument log4netConfig = new XmlDocument();

            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        private static void LoadAppConfig()
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        private static void LoadServices(IServiceCollection services)
        {
            services.AddDbContext<InteractContext>(
                options => options.UseNpgsql(
                     _configuration.GetConnectionString("InteractConnectionString")));

            services.AddDbContext<IdentityContext>(
                options => options.UseNpgsql(
                    _configuration.GetConnectionString("InteractConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();

            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                    _configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromSeconds(int.Parse(tokenConfigurations.Issuer));
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/account/login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                //options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = _Configuration.GetConnectionString("RedisConnectionString");
            //    option.InstanceName = "interact_redis";

            //});

            Startup._services = services.BuildServiceProvider();
        }

        private static void LoadCors(IServiceCollection services)
        {
            ////corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
            //corsBuilder.AllowCredentials();

            services.AddCors(o => o.AddPolicy("SiteCors", builder =>
            {
                builder.AllowCredentials()
                        .AllowAnyOrigin()  // for a any origin
                                           //.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            //});
        }
    }
}
