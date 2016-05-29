using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Finds the first object in a sequence that matches the predicate, 
        /// and returns the index of that object in the sequence.
        /// </summary>
        /// <returns>The position of the matched object, or -1 if no match was found.</returns>
        public static int FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return FindIndex(source, 0, predicate);
        }

        /// <summary>
        /// Finds the first object in a sequence that matches the predicate, 
        /// and returns the index of that object in the sequence.
        /// </summary>
        /// <returns>The position of the matched object, or -1 if no match was found.</returns>
        public static int FindIndex<T>(this IEnumerable<T> source, int startIndex, Func<T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"startIndex was {startIndex}. Value must be greater than or equal to 0.");

            var index = 0;
            foreach (var item in source)
            {
                if (index >= startIndex && predicate(item))
                {
                    return index;
                }
                index++;
            }

            return -1;
        }
    }
}
