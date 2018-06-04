using Interact.Library.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Data.Interface
{
    public interface IConsumerDAL : IDisposable
    {
        ConsumerStatus GetThreadGroupServerStatus(string threadGroup);

        void SetConsumerThreadStatus(string threadGroup, string indentifier, ConsumerStatus clientStatus);
    }
}
