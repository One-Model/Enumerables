using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    /// <summary>
    /// Extension methods for IList&lt;T&gt;
    /// </summary>
    public static partial class ListExtensions
    {
        /// <summary>
        /// Removes all of the items from the list that match a predicate.
        /// </summary>
        public static void Remove<T>(this IList<T> source, Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (var i = source.Count - 1; i >= 0; i--)
            {
                if (predicate(source[i]))
                {
                    source.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Removes all of the items from the list that match a predicate.
        /// Provides the item's index to the predicate function.
        /// </summary>
        public static void Remove<T>(this IList<T> source, Func<T, int, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (var i = source.Count - 1; i >= 0; i--)
            {
                if (predicate(source[i], i))
                {
                    source.RemoveAt(i);
                }
            }
        }
    }
}
