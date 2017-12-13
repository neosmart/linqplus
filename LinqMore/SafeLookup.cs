using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoSmart.Linq
{
    public static class SafeLookup
    {
        /// <summary>
        /// An exception-safe alternative to IEnumerable.First() for non-nullable types. 
        /// (Use <code>IEnumerable.FirstOrDefault()</code> instead for nullable types)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T? FirstOrNull<T>(this IEnumerable<T> container, Func<T, bool> predicate = null) where T : struct
        {
            return predicate == null ? container.Select(t => (T?)t).FirstOrDefault()
                : container.Where(predicate).FirstOrNull();
        }

        /// <summary>
        /// An exception-safe alternative to IEnumerable.Last() for non-nullable types. 
        /// (Use <code>IEnumerable.LastOrDefault()</code> instead for nullable types)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T? LastOrNull<T>(this IEnumerable<T> container, Func<T, bool> predicate = null) where T : struct
        {
            return predicate == null ? container.Select(t => (T?)t).LastOrDefault()
                : container.Where(predicate).LastOrNull();
        }
    }
}
