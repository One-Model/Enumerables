using System;
using System.Collections.Generic;

namespace OneModel.Enumerables
{
    // ReSharper disable once InconsistentNaming
    public static partial class IDictionaryExtensions
    {
        /// <summary>
        /// Compares two dictionaries to see if they have the same 
        /// keys, and the same values for each key. If no valueComparer
        /// is supplied, uses the default equality comparer.
        /// </summary>
        public static bool DictionaryEquals<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> source, 
            IReadOnlyDictionary<TKey, TValue> other, 
            IEqualityComparer<TValue> valueComparer = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;

            if (source.Count != other.Count)
            {
                return false;
            }

            foreach (var leftPair in source)
            {
                var key = leftPair.Key;
                var leftValue = leftPair.Value;

                TValue rightValue;
                if (!other.TryGetValue(key, out rightValue))
                {
                    return false;
                }

                if (!valueComparer.Equals(leftValue, rightValue))
                {
                    return false;
                }
            }

            return true;
        }
    }
}