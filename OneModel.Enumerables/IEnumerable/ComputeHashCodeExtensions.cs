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
        /// Computes the hashcode for an enumerable. Assumes that a suitable implementation
        /// of GetHashCode has been implemented for type T.
        /// </summary>
        public static int ComputeHashCode<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            unchecked
            {
                return source.Aggregate(0, (current, item) => (current * 397) ^ item.GetHashCode());
            }
        }
    }
}
