using Interact.Instance.Data.Interface;
using Interact.Library.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Data.Postgresql.Data
{
    public class ConsumerDAL : IConsumerDAL
    {
        public void Dispose()
        {
            this.Dispose();
        }

        public ConsumerStatus GetThreadGroupServerStatus(string threadGroup)
        {
            throw new NotImplementedException();
        }

        public void SetConsumerThreadStatus(string threadGroup, string indentifier, ConsumerStatus clientStatus)
        {
            throw new NotImplementedException();
        }
    }
}
