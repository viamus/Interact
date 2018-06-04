using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Data.Postgresql.InteractDomain.Security
{
    public class TokenAuthOption
    {
        public string Audience { get; set; } = "MyAudience";

        public string Issuer { get; set; } = "MyIssuer";

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(40);

        public static string TokenType { get; } = "Bearer";

        public SigningCredentials SigningCredentials { get; set; }
    }
}
