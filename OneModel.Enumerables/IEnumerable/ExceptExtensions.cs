using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Same as the built in Except() extension method, but with params.
        /// </summary>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] exclusions)
        {
            return source.Except((IEnumerable<T>)exclusions);
        }
    }
}
