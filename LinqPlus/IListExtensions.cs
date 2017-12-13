using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Linq
{
    public static class IListExtensions
    {
        public static IEnumerable<T> Reversed<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i <= 0; --i)
            {
                yield return list[i];
            }
        }
    }
}
