using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.Linq
{
    /// <summary>
    /// Just like System.Collection.Generic.Dictionary, except returns an expandable tuple on enumeration
    /// </summary>
    public class TupleDictionary<K, V> : Dictionary<K, V>
    {
        public new IEnumerator<(K Key, V Value)> GetEnumerator()
        {
            return base.GetEnumerator().AsTuples();
        }
    }

    public class TupleEnumerator<K, V> : IEnumerator<(K Key, V Value)>
    {
        private IEnumerator<KeyValuePair<K, V>> _underlying;

        public TupleEnumerator(IEnumerator<KeyValuePair<K, V>> kvEnumerator)
        {
            _underlying = kvEnumerator;
        }

        public (K Key, V Value) Current => (_underlying.Current.Key, _underlying.Current.Value);

        object IEnumerator.Current => (_underlying.Current.Key, _underlying.Current.Value);

        public void Dispose()
        {
            _underlying.Dispose();
        }

        public bool MoveNext()
        {
            return _underlying.MoveNext();
        }

        public void Reset()
        {
            _underlying.Reset();
        }
    }

    public static class TupleExtensionMethods
    {
        public static TupleEnumerator<K, V> AsTuples<K, V>(this IEnumerator<KeyValuePair<K, V>> enumerator)
        {
            return new TupleEnumerator<K, V>(enumerator);
        }

        public static IEnumerable<(K Key, V Value)> Tuples<K, V>(this IEnumerable<KeyValuePair<K, V>> enumerable)
        {
            foreach (var kv in enumerable)
            {
                yield return (kv.Key, kv.Value);
            }
        }

        public static (K Key, V Value) AsTuple<K, V>(this KeyValuePair<K, V> kv)
        {
            return (kv.Key, kv.Value);
        }
    }
}
