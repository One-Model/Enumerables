using System;
using System.Collections.Generic;
using System.Linq;

namespace OneModel.Enumerables.ICollection
{
    public static class AddIfNotPresentExtensions
    {
        /// <summary>
        /// Adds an item into the collection, if it's not already present.
        /// Uses the default equality comparer.
        /// </summary>
        public static void AddIfNotPresent<TItem>(this ICollection<TItem> source, TItem item)
        {
            if (!source.Contains(item))
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// Adds an item into the collection, if it's not already present.
        /// Uses a function to extract a key from each item, and compares the
        /// keys using the .Equals method.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <param name="keyFn"></param>
        public static void AddIfNotPresent<TItem, TKey>(this ICollection<TItem> source, TItem item, Func<TItem, TKey> keyFn)
        {
            var itemKey = keyFn(item);
            if (!source.Any(s => keyFn(s).Equals(itemKey)))
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// Adds an item into the collection, if it's not already present.
        /// Uses the provided equality comparer.
        /// </summary>
        public static void AddIfNotPresent<TItem>(this ICollection<TItem> source, TItem item, IEqualityComparer<TItem> comparer)
        {
            if (!source.Contains(item, comparer))
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// Adds an item into the collection, if it's not already present.
        /// Uses the provided function to compare items.
        /// </summary>
        public static void AddIfNotPresent<TItem>(this ICollection<TItem> source, TItem item, Func<TItem,TItem, bool> comparer)
        {
            if (!source.Any(s => comparer(s, item)))
            {
                source.Add(item);
            }
        }
    }
}