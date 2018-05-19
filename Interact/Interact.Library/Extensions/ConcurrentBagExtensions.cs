using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interact.Library
{
    public static class ConcurrentBagExtensions
    {
        public static void AddRange<T>(this ConcurrentBag<T> bag, ICollection<T> toAdd)
        {
            if (!toAdd.NullOrEmpty())
            {
                toAdd.AsParallel().ForAll(t => bag.Add(t));
            }
        }
    }
}
