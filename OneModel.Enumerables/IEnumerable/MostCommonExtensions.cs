using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Finds the most common occurance of T in an enumerable.
        /// </summary>
        public static T MostCommon<T>(this IEnumerable<T> list)
        {
            return list.GroupBy(i => i)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => grp.Key)
                .First();
        }
    }
}
