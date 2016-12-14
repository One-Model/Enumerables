using System.Collections.Generic;
using System.Linq;

namespace OneModel.Enumerables.IEnumerable
{
    public static class ToReadOnlyCollectionExtensions
    {
        /// <summary>
        /// Converts an enumerable to an IReadOnlyCollection&lt;T&gt;
        /// Useful if you want to be able to reason about the length of
        /// a enumerable and want to avoid multiple enumerations, and don't
        /// care if the source is an array, list or whatever.
        /// </summary>
        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
        {
            return source as IReadOnlyCollection<T> ?? source.ToArray();
        }
    }
}
