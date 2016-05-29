using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class ICollectionExtensions
    {
        /// <summary>
        /// Adds all of the objects in items to source.
        /// </summary>
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// Adds all of the objects in items to source.
        /// </summary>
        public static void AddRange<T>(this ICollection<T> source, params T[] items)
        {
            source.AddRange((IEnumerable<T>)items);
        }
    }
}
