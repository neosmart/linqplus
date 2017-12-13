using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Linq
{
    /// <summary>
    /// Optimized versions of LINQ routines for SortedSet&lt;T&gt; containers, and additional set-specific operations.
    /// </summary>
    public static class SortedSetExtensions
    {
        public static IEnumerable<T> Distinct<T>(this SortedSet<T> container)
        {
            //already is
            return container;
        }

        private static void Split<T>(SortedSet<T> container1, SortedSet<T> container2, out SortedSet<T> before, out SortedSet<T> overlapping, out SortedSet<T> after)
        {
            before = container1.GetViewBetween(container1.Min, container2.Min);
            overlapping = container1.GetViewBetween(container2.Min, container2.Max);
            after = container1.GetViewBetween(container2.Max, container1.Max);
        }

        public static IEnumerable<T> Except<T>(this SortedSet<T> container1, SortedSet<T> container2)
        {
            Split(container1, container2, out var before, out var overlapping, out var after);

            foreach (var t in before)
            {
                yield return t;
            }
            foreach (var t in overlapping)
            {
                if (!container1.Contains(t))
                {
                    yield return t;
                }
            }
            foreach (var t in after)
            {
                yield return t;
            }
        }

        public static T Last<T>(this SortedSet<T> container)
        {
            return container.Max;
        }

        public static IEnumerable<T> Union<T>(this SortedSet<T> container1, SortedSet<T> container2) where T : IComparable<T>
        {
            //if they are disjoint sets, just concat them both
            if (container1.Max.CompareTo(container2.Min) < 0)
            {
                foreach (var t in container1)
                {
                    yield return t;
                }
                foreach (var t in container2)
                {
                    yield return t;
                }
                yield break;
            }
            if (container1.Max.CompareTo(container1.Min) < 0)
            {
                foreach (var t in container2)
                {
                    yield return t;
                }
                foreach (var t in container1)
                {
                    yield return t;
                }
                yield break;
            }

            Split(container1, container2, out var before, out var overlapping, out var after);

            //return everything in c1 before c2
            foreach (var t in before)
            {
                yield return t;
            }

            //return everything in c1's overlapping range with c2
            foreach (var t in overlapping)
            {
                yield return t;
            }

            //return items in c2 not in c1
            foreach (var t in container2)
            {
                if (!overlapping.Contains(t))
                {
                    yield return t;
                }
            }

            foreach (var t in after)
            {
                yield return t;
            }
        }
    }
}
