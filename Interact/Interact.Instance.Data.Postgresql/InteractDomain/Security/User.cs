using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Interact.Instance.Data.Postgresql.InteractDomain.Security
{
    public class User
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

    public static class Roles
    {
        public const string ROLE_API_ACCOUNT = "Acesso-API-Account";
    }
    }


}
