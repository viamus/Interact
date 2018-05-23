using System;

namespace Interact.Instance.Data.Exceptions
{
    public class NotFoundException : Exception
    {

        public NotFoundException()
          : base()
        {
        }

        public NotFoundException(String message)
          : base(message)
        {
        }

        public NotFoundException(String message, Exception innerException)
          : base(message, innerException)
        {
        }

    }
}
