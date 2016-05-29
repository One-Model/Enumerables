using System.Collections.Generic;

namespace OneModel.Enumerables
{
    /// <summary>
    /// A replacement for the built in ILookup that provides
    /// a more complete interface.
    /// </summary>
    public interface IIndex<TKey, TValue>
    {
        /// <summary>
        /// Determines if the given key is present in the index.
        /// </summary>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Determines if the given value is stored in this index,
        /// under any key.
        /// </summary>
        bool ContainsValue(TValue value);

        /// <summary>
        /// Determines if the given value is stored with the given
        /// key in this index.
        /// </summary>
        bool Contains(TKey key, TValue value);

        /// <summary>
        /// Adds a value to this lookup, with the given key.
        /// </summary>
        void Add(TKey key, TValue value);

        /// <summary>
        /// Clears all keys and values from this index.
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears all values in this lookup that are stored
        /// under the given key.
        /// </summary>
        void Clear(TKey key);

        /// <summary>
        /// Returns the values that are stored under the given key.
        /// </summary>
        List<TValue> this[TKey key] { get; }

        /// <summary>
        /// Returns all of the keys in this index.
        /// </summary>
        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// Returns all of the key/value pairs in this index.
        /// </summary>
        IEnumerable<KeyValuePair<TKey, List<TValue>>> Collections { get; }

        /// <summary>
        /// The values stored in this index.
        /// </summary>
        IEnumerable<List<TValue>> Values { get; }

        /// <summary>
        /// Removes a key from this index.
        /// </summary>
        void Remove(TKey key);

        /// <summary>
        /// Removes an item from this index.
        /// </summary>
        void Remove(TKey key, TValue value);

        /// <summary>
        /// Removes all occurrences of an item from this index.
        /// </summary>
        void Remove(TValue value);
    }
}
