using System;
using System.Collections.Generic;

namespace OneModel.Enumerables
{
    // ReSharper disable once InconsistentNaming
    public static partial class IDictionaryExtensions
    {
        /// <summary>
        /// Retrieves the item with the given key from the dictionary. If no item
        /// is found, then the creator function will be used to create a new
        /// instance, add it to the dictionary, and then that new instance will
        /// be returned.
        /// </summary>
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> creator)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (creator == null)
            {
                throw new ArgumentNullException(nameof(creator));
            }

            TValue existing;
            if (!dictionary.TryGetValue(key, out existing))
            {
                existing = creator();
                dictionary[key] = existing;
            }

            return existing;
        }

        /// <summary>
        /// Retrieves the item with the given key from the dictionary. If no item
        /// is found, then a new instance of TValue will be created using the 
        /// parameterless constructor, added to the dictionary, and then
        /// returned.
        /// </summary>
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            return dictionary.GetOrCreate(key, () => new TValue());
        }
    }
}