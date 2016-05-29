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
        /// Removes and returns a T item from a list. 
        /// </summary>
        /// <returns>Returns matching item, or default value for T when no item found.</returns>
        public static T TakeAndRemove<T>(this IList<T> list, T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var index = list.IndexOf(item);

            if (index == -1)
                return default(T);

            var result = list[index];
            list.RemoveAt(index);

            return result;
        }

        /// <summary>
        /// Removes and returns a T item from a list that match a predicate. 
        /// </summary>
        /// <returns>Returns matching item, or default value for T when no item found.</returns>
        public static T TakeAndRemove<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (!predicate(list[i]))
                    continue;

                var result = list[i];
                list.RemoveAt(i);
                return result;
            }

            return default(T);
        }
    }
}
