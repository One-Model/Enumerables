using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Given two sets of IEnumerables, figures out which items are in the left set, which are in the right set, and which
        /// are in both sets. Assumes that both sets of items can be uniquely identified by a key, and that equality comparisons
        /// between the keys of the left side and the keys of the right side are meaningful.
        /// </summary>
        public static VennResult<TLeft, TRight> Venn<TLeft, TRight, TKey>(this IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TKey> leftKeyFunc, Func<TRight, TKey> rightKeyFunc)
        {
            if (right == null)
                throw new ArgumentNullException(nameof(right));
            if (leftKeyFunc == null)
                throw new ArgumentNullException(nameof(leftKeyFunc));
            if (rightKeyFunc == null)
                throw new ArgumentNullException(nameof(rightKeyFunc));

            var l = left.ToList();
            var b = new List<VennResult<TLeft, TRight>.Pair>();

            var rDict = right.ToDictionary(rightKeyFunc);

            for (var i = l.Count - 1; i >= 0; i--)
            {
                var lKey = leftKeyFunc(l[i]);
                TRight match;
                if (rDict.TryGetValue(lKey, out match))
                {
                    // Item exists on both sides.
                    b.Add(new VennResult<TLeft, TRight>.Pair(l[i], match));
                    l.RemoveAt(i);
                    rDict.Remove(lKey);
                }
            }

            var r = rDict.Values.ToList();

            return new VennResult<TLeft, TRight>(l, r, b);
        }

        /// <summary>
        /// Same as <see cref="Venn{TLeft, TRight, TKey}(IEnumerable{TLeft}, IEnumerable{TRight}, Func{TLeft, TKey}, Func{TRight, TKey})"/> 
        /// except it accepts two sets of the same type, and so only requires one key selector which is applied
        /// to both sets.
        /// </summary>
        public static VennResult<T, T> Venn<T, TKey>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, TKey> keySelector)
            => left.Venn(right, keySelector, keySelector);

        /// <summary>
        /// Same as <see cref="Venn{T, TKey}(IEnumerable{T}, IEnumerable{T}, Func{T, TKey})"/> 
        /// except the objects in each set are their own keys, which can be compared meaningfully.
        /// </summary>
        public static VennResult<T, T> Venn<T>(this IEnumerable<T> left, IEnumerable<T> right) => left.Venn(right, x => x);
    }
}