using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Interact.Library
{
    public static class ConcurrentQueueExtensions
    {
        public static void EnqueueRange<T>(this ConcurrentQueue<T> queue, ICollection<T> toAdd)
        {
            if (!toAdd.NullOrEmpty())
            {
                toAdd.AsParallel().ForAll(t => queue.Enqueue(t));
            }
        }
    }
}
