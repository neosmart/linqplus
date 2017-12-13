﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoSmart.Linq
{
    public static class ExtendedLinq
    {
        public static bool ContainsAnyOf<T>(this IEnumerable<T> container, IEnumerable<T> values)
        {
            return ContainsAnyOf(container, new SortedSet<T>(values));
        }

        public static bool ContainsAnyOf<T>(this IEnumerable<T> container, SortedSet<T> values)
        {
            foreach(var value in container)
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
            foreach(var value in values)
            {
                if (!container.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool In<T>(this T t, IEnumerable<T> container)
        {
            return container.Contains(t);
        }

        public static bool Empty<T>(this IEnumerable<T> container)
        {
            return !container.Any();
        }
    }
}
