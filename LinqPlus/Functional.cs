using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoSmart.Linq
{
    public static class Functional
    {
        public static void ForEach<T>(this IEnumerable<T> container, Action<T> action)
        {
            foreach (var t in container)
            {
                action(t);
            }
        }

        public static int Remove<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            var toRemove = collection.Where(predicate).ToArray();

            foreach (var t in toRemove)
            {
                collection.Remove(t);
            }

            return toRemove.Length;
        }

        public static int Remove<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            var indices = new List<int>();

            var i = 0;
            //add forwards...
            foreach (var value in collection)
            {
                if (predicate(value))
                {
                    indices.Add(i++);
                }
            }

            //... but remove backwards, so it's safe to modify in one go
            foreach (var j in indices.Reversed())
            {
                collection.RemoveAt(j);
            }

            return indices.Count;
        }

        //To-do: optimized Remove<T>() for List<T>, similar to IList<T> but using RemoveRange() instead
    }
}
