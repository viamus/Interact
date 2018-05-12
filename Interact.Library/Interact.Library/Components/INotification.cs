using Interact.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Library.Components
{
    public interface INotification<T>
    {
        void Notify(T payload, WorkerResult workerResult);
    }
}
