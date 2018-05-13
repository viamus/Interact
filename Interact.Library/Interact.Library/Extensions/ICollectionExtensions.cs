using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Interact.Library
{
    public static class ICollectionExtensions
    {
        public static bool NullOrEmpty<T>(this ICollection<T> objects)
        {
            return !(objects!=null && objects.Count() > 0);
        }
    }
}
