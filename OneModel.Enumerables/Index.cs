using System.Collections.Generic;
using System.Linq;

namespace OneModel.Enumerables
{
    /// <summary>
    /// Default implementation of IIndex, backed by a Dictionary.
    /// </summary>
    public class Index<TKey, TValue> : IIndex<TKey, TValue>
    {
        private readonly Dictionary<TKey, List<TValue>> _storage = new Dictionary<TKey, List<TValue>>();

        /// <summary>
        /// Determines if the given key is present in the lookup.
        /// </summary>
        public bool ContainsKey(TKey key)
        {
            return _storage.ContainsKey(key);
        }

        /// <summary>
        /// Determines if the given value is stored in this lookup,
        /// under any key.
        /// </summary>
        public bool ContainsValue(TValue value)
        {
            return _storage.Values.Any(v => v.Contains(value));
        }

        /// <summary>
        /// Determines if the given value is stored with the given
        /// key in this lookup.
        /// </summary>
        public bool Contains(TKey key, TValue value)
        {
            List<TValue> values;
            return _storage.TryGetValue(key, out values) && values.Contains(value);
        }

        /// <summary>
        /// Adds a value to this lookup, with the given key.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            List<TValue> values;
            if (_storage.TryGetValue(key, out values))
            {
                values.Add(value);
            }
            else
            {
                _storage[key] = new List<TValue> { value };
            }
        }

        /// <summary>
        /// Clears all keys and values from this lookup.
        /// </summary>
        public void Clear()
        {
            _storage.Clear();
        }

        /// <summary>
        /// Clears all values in this lookup that are stored
        /// under the given key.
        /// </summary>
        public void Clear(TKey key)
        {
            List<TValue> values;
            if (_storage.TryGetValue(key, out values))
            {
                values.Clear();
            }
        }

        /// <summary>
        /// Returns the values that are stored under the given key.
        /// </summary>
        public List<TValue> this[TKey key]
        {
            get
            {
                List<TValue> values;
                return _storage.TryGetValue(key, out values) ? values : null;
            }
        }

        /// <summary>
        /// Returns all of the keys in this lookup.
        /// </summary>
        public IEnumerable<TKey> Keys => _storage.Keys;

        /// <summary>
        /// The values stored in this lookup.
        /// </summary>
        public IEnumerable<List<TValue>> Values => _storage.Values;

        /// <summary>
        /// Returns all of the key/value pairs in this index.
        /// </summary>
        public IEnumerable<KeyValuePair<TKey, List<TValue>>> Collections => _storage;

        /// <summary>
        /// Removes a key from this index.
        /// </summary>
        public void Remove(TKey key)
        {
            _storage.Remove(key);
        }

        /// <summary>
        /// Removes an item from this index.
        /// </summary>
        public void Remove(TKey key, TValue value)
        {
            List<TValue> values;
            if (_storage.TryGetValue(key, out values))
            {
                values.Remove(value);
            }
        }

        /// <summary>
        /// Removes all occurrences of an item from this index.
        /// </summary>
        public void Remove(TValue value)
        {
            foreach (var list in _storage.Values)
            {
                list.Remove(value);
            }
        }
    }
}
