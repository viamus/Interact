using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Data.Postgresql.InteractDomain.Security
{
    public class RefreshTokenData
    {
        public string RefreshToken { get; set; }

        public string UserName { get; set; }
    }
}
