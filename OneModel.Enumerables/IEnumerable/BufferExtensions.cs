using System;
using System.Collections.Generic;

namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Buffers access to an enumerable, allowing lazy read access to it, 
        /// and ensuring that the source enumerable is read at most once.
        /// </summary>
        public static BufferedEnumerable<T> Buffer<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new BufferedEnumerable<T>(source);
        }
    }
}