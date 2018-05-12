using Interact.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Library.Components
{
    public interface IWorker<T>
    {
        void DoWork(T payload, Endpoint endpoint, INotification<T> notify );
    }
}
