using System.Collections.Generic;

namespace OneModel.Enumerables.ICollection
{
    public static class IsNullOrEmptyExtensions
    {
        /// <summary>
        /// Returns true for an empty or null collection.
        /// </summary>
        public static bool IsNullOrEmpty<T> (this ICollection<T> collection)
        {
            if (collection == null)
                return true;

            return collection.Count < 1;
        }
    }
}
