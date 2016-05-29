// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public enum DistinctBehaviour
    {
        /// <summary>
        /// Throw an exception if a duplicate is detected.
        /// </summary>
        ThrowExceptionOnDuplicate = 1,

        /// <summary>
        /// Remove duplicates, and keep only the first item found with a particular key.
        /// </summary>
        KeepFirst = 2,

        /// <summary>
        /// Remove duplicates, and keep only the last item found with a particular key.
        /// </summary>
        KeepLast = 3
    }
}
