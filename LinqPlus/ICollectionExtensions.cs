using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Linq
{
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Optimized .Any() routine to check for an empty collection;
        /// will not throw an InvalidOperationException if the collection was modified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool Any<T>(this ICollection<T> collection)
        {
            return collection.Count != 0;
        }

        public static bool Empty<T>(this ICollection<T> collection)
        {
            return !collection.Any();
        }

        public static bool None<T>(this ICollection<T> collection)
        {
            return !collection.Any();
        }

        /// <summary>
        /// Optimized .Count() routine to check for an empty collection;
        /// will not throw an InvalidOperationException if the collection was modified.
        /// </summary>
        public static int Count<T>(this ICollection<T> collection)
        {
            return collection.Count;
        }

        public static bool ContainsAnyOf<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                if (collection.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAllOf<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                if (!collection.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
