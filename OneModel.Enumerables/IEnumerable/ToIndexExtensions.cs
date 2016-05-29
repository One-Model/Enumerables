using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        public static IIndex<TKey, TValue> ToIndex<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, IEnumerable<TKey>> keySelector)
        {
            return ToIndex(source, keySelector, item => item);
        }

        public static IIndex<TKey, TValue> ToIndex<TIn, TKey, TValue>(this IEnumerable<TIn> source, Func<TIn, IEnumerable<TKey>> keySelector, Func<TIn, TValue> valueSelector)
        {
            var index = new Index<TKey, TValue>();

            foreach (var item in source)
            {
                var keys = keySelector(item);
                var value = valueSelector(item);

                foreach (var key in keys)
                {
                    index.Add(key, value);
                }
            }

            return index;
        }

        public static IIndex<TKey, TValue> ToIndex<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
        {
            return ToIndex(source, keySelector, item => item);
        }

        public static IIndex<TKey, TValue> ToIndex<TIn, TKey, TValue>(this IEnumerable<TIn> source, Func<TIn, TKey> keySelector, Func<TIn, TValue> valueSelector)
        {
            var index = new Index<TKey, TValue>();

            foreach (var item in source)
            {
                var key = keySelector(item);
                var value = valueSelector(item);
                index.Add(key, value);
            }

            return index;
        }
    }
}
