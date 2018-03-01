using System;
using System.Collections.Generic;

namespace OneModel.Enumerables
{
    public static class SplitExtensions
    {
        /// <summary>
        /// Splits a sequence of items on items that match a predicate.
        /// </summary>
        public static IEnumerable<IReadOnlyList<T>> Split<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var current = new List<T>();

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return current;
                    current = new List<T>();
                }
                else
                {
                    current.Add(item);
                }
            }
            
            yield return current;
        }
    }
}