using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Linq
{
    public static class ICollectionExtensions
    {
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
