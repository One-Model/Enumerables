using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    // ReSharper disable once InconsistentNaming
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Concatenates two sequences. Similar to the built-in LINQ 
        /// Concat(), except with a params array.
        /// </summary>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, params T[] items)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return Enumerable.Concat(source, items);
        }
    }
}