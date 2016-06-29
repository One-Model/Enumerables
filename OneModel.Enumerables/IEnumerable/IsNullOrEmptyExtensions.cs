using System.Collections.Generic;
using System.Linq;

namespace OneModel.Enumerables.IEnumerable
{
    public static class IsNullOrEmptyExtensions
    {
        /// <summary>
        /// Returns true for an empty or null collection.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }

            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }
    }
}
