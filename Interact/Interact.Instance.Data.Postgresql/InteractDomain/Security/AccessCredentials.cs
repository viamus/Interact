using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Data.Postgresql.InteractDomain.Security
{
    public class AccessCredentials
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string RefreshToken { get; set; }

        public string GrantType { get; set; }
    }
}
