using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Exceptions
{ 
    public class AWSQueueMessageException : Exception
    {

        public AWSQueueMessageException()
          : base()
        {
        }

        public AWSQueueMessageException(String message)
          : base(message)
        {
        }

        public AWSQueueMessageException(String message, Exception innerException)
          : base(message, innerException)
        {
        }

    }
}
