using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    /// <summary>
    /// A function that can take an existing item, and a duplicate of it, and figure out which one to keep.
    /// It's also possible to return an entirely new object, if required.
    /// </summary>
    public delegate T DuplicateResolver<T>(T existing, T duplicate);

    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Finds the distinct items in a sequence, with configuration to dictate what to do
        /// when duplicates are found.
        /// </summary>
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, DistinctBehaviour behaviour = DistinctBehaviour.ThrowExceptionOnDuplicate)
        {
            switch (behaviour)
            {
                case DistinctBehaviour.ThrowExceptionOnDuplicate:
                    return source.DistinctThrowException(keySelector);
                case DistinctBehaviour.KeepFirst:
                    return source.DistinctKeepFirst(keySelector);
                case DistinctBehaviour.KeepLast:
                    return source.DistinctKeepLast(keySelector);
                default:
                    throw new ArgumentOutOfRangeException(nameof(behaviour), behaviour, null);
            }
        }

        /// <summary>
        /// Finds the distinct items in a sequence. Uses a DuplicateResolver to resolve any duplicate that 
        /// may be found.
        /// </summary>
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, DuplicateResolver<T> resolver)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver));

            var found = new Dictionary<TKey, T>();
            foreach (var item in source)
            {
                var key = keySelector(item);
                T existing;
                if (found.TryGetValue(key, out existing))
                {
                    found[key] = resolver(existing, item);
                }
                else
                {
                    found[key] = item;
                }
            }

            return found.Values;
        }

        /// <summary>
        /// Finds the distinct items in a sequence. Uses a function to extract a key from the object that should be unique.
        /// If a duplicate is found, an exception is thrown.
        /// </summary>
        public static IEnumerable<T> DistinctThrowException<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var found = new HashSet<TKey>();
            foreach (var item in source)
            {
                var key = keySelector(item);
                if (found.Contains(key))
                {
                    throw new Exception($"Duplicate key found in source. Key was: {key}.");
                }

                found.Add(key);
                yield return item;
            }
        }

        /// <summary>
        /// Finds the distinct items in a sequence. Uses a function to extract a key from the object that should be unique.
        /// Duplicates are ignored, and only the first item that matches a key is returned.
        /// </summary>
        public static IEnumerable<T> DistinctKeepFirst<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var found = new HashSet<TKey>();
            foreach (var item in source)
            {
                var key = keySelector(item);
                if (!found.Contains(key))
                {
                    found.Add(key);
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Finds the distinct items in a sequence. Uses a function to extract a key from the object that should be unique.
        /// Duplicates are ignored, and only the last item that matches a key is returned.
        /// </summary>
        public static IEnumerable<T> DistinctKeepLast<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var found = new Dictionary<TKey, T>();
            foreach (var item in source)
            {
                var key = keySelector(item);
                found[key] = item;
            }

            return found.Values;
        }
    }
}
