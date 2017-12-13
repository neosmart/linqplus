using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Linq
{
    //inspired by http://stackoverflow.com/a/4576732/17027
    public static class UnorderedSequence
    {
        //optimization for ICollection<T> in case of mismatching counts
        public static bool UnorderedSequenceEquals<T>(this ICollection<T> first, ICollection<T> second, IEqualityComparer<T> comparer = null)
        {
            if (first.Count != second.Count)
            {
                return false;
            }

            return UnorderedSequenceEquals((IEnumerable<T>)first, (IEnumerable<T>)second, comparer, first.Count);
        }

        /// <summary>
        /// Returns <code>true</code> if the contents of the two containers <paramref name="sequence1"/> and <paramref name="sequence2"/> 
        /// are identical, even if in a different order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence1"></param>
        /// <param name="sequence2"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool UnorderedSequenceEquals<T>(this IEnumerable<T> sequence1, IEnumerable<T> sequence2, IEqualityComparer<T> comparer = null)
        {
            return UnorderedSequenceEquals(sequence1, sequence2, comparer);
        }

        public static bool UnorderedSequenceEquals<T>(this IEnumerable<T> sequence1, IEnumerable<T> sequence2, IEqualityComparer<T> comparer = null, int capacityHint = 0)
        {
            //count the number of each instance
            Dictionary<T, int> counts;
            if (capacityHint == 0)
            {
                counts = new Dictionary<T, int>(comparer);
            }
            else
            {
                counts = new Dictionary<T, int>(capacityHint, comparer);
            }

            var toRemove = 0;
            foreach (var i in sequence1)
            {
                if (counts.TryGetValue(i, out var c))
                {
                    counts[i] = c + 1;
                }
                else
                {
                    counts[i] = 1;
                    ++toRemove;
                }
            }

            var removed = 0; //track number of unique items that cancelled out between the two sequences
            foreach (var i in sequence2)
            {
                if (!counts.TryGetValue(i, out var c))
                {
                    //early termination
                    return false;
                }

                if (c == 1)
                {
                    counts.Remove(i);
                    ++removed;
                    continue;
                }
                counts[i] = c - 1;
            }

            return removed == toRemove;
        }
    }
}
