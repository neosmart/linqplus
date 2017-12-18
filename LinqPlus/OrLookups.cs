using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoSmart.Linq
{
    public static class OrLookups
    {
        public static T FirstOr<T>(this IEnumerable<T> container, T or)
        {
            foreach (var t in container)
            {
                return t;
            }

            return or;
        }

        public static T FirstOr<T>(this IEnumerable<T> container, Func<T, bool> where, T or)
        {
            foreach (var t in container.Where(where))
            {
                return t;
            }

            return or;
        }

        public static T LastOr<T>(this IEnumerable<T> container, T or)
        {
            T last = or;
            foreach (var t in container)
            {
                last = t;
            }

            return last;
        }

        public static T LastOr<T>(this IEnumerable<T> container, Func<T, bool> where, T or)
        {
            T last = or;
            foreach (var t in container.Where(where))
            {
                last = t;
            }

            return last;
        }
    }
}
