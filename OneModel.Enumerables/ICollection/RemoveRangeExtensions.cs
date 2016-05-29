using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class ICollectionExtensions
    {
        /// <summary>
        /// Removes all of the objects that exist in items from source.
        /// </summary>
        public static void RemoveRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                source.Remove(item);
            }
        }

        /// <summary>
        /// Removes all of the objects that exist in items from source.
        /// NOTE: Need to be careful with this overload, because List&lt;T&gt; has a RemoveRange(int, int) method.
        /// </summary>
        public static void RemoveRange<T>(this ICollection<T> source, params T[] items)
        {
            source.RemoveRange((IEnumerable<T>)items);
        }
    }
}
