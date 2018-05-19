using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Exceptions
{
    public class RedisLockedKeyException : Exception
    {

        public RedisLockedKeyException()
          : base()
        {
        }

        public RedisLockedKeyException(String message)
          : base(message)
        {
        }

        public RedisLockedKeyException(String message, Exception innerException)
          : base(message, innerException)
        {
        }
    
    }
}
