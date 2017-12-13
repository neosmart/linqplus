using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Linq
{
    /// <summary>
    /// Optimized versions of LINQ routines for SortedSet&lt;T&gt; containers, and additional set-specific operations.
    /// </summary>
    public static class IDictionaryExtensions
    {
        public static bool ContainsAnyOf<K, V>(this IDictionary<K,V> dictionary, IEnumerable<K> values)
        {
            foreach(var value in values)
            {
                if (dictionary.ContainsKey(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAllOf<K, V>(this IDictionary<K, V> dictionary, IEnumerable<K> values)
        {
            foreach (var value in values)
            {
                if (!dictionary.ContainsKey(value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
