using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoSmart.Linq
{
    /// <summary>
    /// Optimized methods to return the last <code>n</code> elements in an enumerable.
    /// </summary>
    public static class LastN
    {
        public static IEnumerable<T> Last<T>(this IEnumerable<T> collection, int n)
        {
            var buffer = new Queue<T>(n);

            foreach (var t in collection)
            {
                buffer.Enqueue(t);
                if (buffer.Count == n)
                {
                    buffer.Dequeue();
                }
            }

            return buffer;
        }

        public static IEnumerable<T> Last<T>(this ICollection<T> collection, int n)
        {
            if (collection.Count <= n)
            {
                return collection;
            }

            return collection.Skip(collection.Count - n);
        }
    }
}
