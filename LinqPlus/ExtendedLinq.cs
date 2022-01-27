using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoSmart.Linq
{
    public static class ExtendedLinq
    {
        public static bool Contains<T>(this IEnumerable<T> container, Func<T, bool> where)
        {
            return container.Where(where).Any();
        }

        public static bool ContainsAnyOf<T>(this IEnumerable<T> container, IEnumerable<T> values)
        {
            return ContainsAnyOf(container, new SortedSet<T>(values));
        }

        public static bool ContainsAnyOf<T>(this IEnumerable<T> container, SortedSet<T> values)
        {
            foreach (var value in container)
            {
                if (container.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAllOf<T>(this IEnumerable<T> container, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                if (!container.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool None<T>(this IEnumerable<T> container)
        {
            return container.Empty();
        }

        public static bool None<T>(this IEnumerable<T> container, Func<T, bool> where)
        {
            return !Contains(container, where);
        }

        public static bool In<T>(this T t, IEnumerable<T> container)
        {
            return container.Contains(t);
        }

        public static bool In<T>(this T t, params T[] values)
            where T : IEquatable<T>
        {
            for (int i = 0; i < values.Length; ++i)
            {
                if (values.Equals(t))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Empty<T>(this IEnumerable<T> container)
        {
            return !container.Any();
        }

// .NET 6 *finally* introduced these, so we can drop them in that implementation
#if !NET6_0_OR_GREATER
        public static T MaxBy<T,B>(this IEnumerable<T> enumerable, Func<T,B> selector)
            where B: IComparable<B>
        {
            T maxT = enumerable.First();
            B maxB = selector(maxT);

            foreach (var t in enumerable.Skip(1))
            {
                if (selector(t).CompareTo(maxB) > 0)
                {
                    maxT = t;
                }
            }

            return maxT;
        }

        public static T MinBy<T, B>(this IEnumerable<T> enumerable, Func<T, B> selector)
            where B : IComparable<B>
        {
            T minT = enumerable.First();
            B minB = selector(minT);

            foreach (var t in enumerable.Skip(1))
            {
                if (selector(t).CompareTo(minB) < 0)
                {
                    minT = t;
                }
            }

            return minT;
        }
#endif
    }
}
