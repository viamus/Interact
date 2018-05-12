using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Library.Components
{
    public interface IConsumer
    {
        ICollection<T> GetObjectsFromQueue<T>();

        bool RemoveObjectsFromQueue<T>(ICollection<T> objects);
    }
}
