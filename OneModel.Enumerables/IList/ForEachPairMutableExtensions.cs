using System;
using System.Collections.Generic;

namespace OneModel.Enumerables
{
    public static partial class ListExtensions
    {
        /// <summary>
        /// Iterates over every pair of values in a list. Allows items to
        /// be efficiently removed or replaced when iterating, while ensuring
        /// that the absolute minimum number of pairs is generated. When items
        /// are removed, they won't be part of any future pairings. When items
        /// are replaced, the old item won't be part of any future pairings,
        /// and any previous pairings involving the old item will be generated
        /// again with the new item.
        /// </summary>
        public static void ForEachPairMutable<T>(this IList<T> source, Action<IForeachPairMutableContext<T>> callback)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var nextIndexToCompare = -1;
            var indicesToCompare = new List<bool>(source.Count);
            for (var i = 0; i < source.Count; i++)
            {
                indicesToCompare.Add(true);
            }

            while (MoveToNextIndexToCompare(indicesToCompare, ref nextIndexToCompare))
            {
                var i = nextIndexToCompare;
                var lhsHasChanged = false;
                for (var j = 0; j < source.Count && !lhsHasChanged; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    indicesToCompare[i] = false;

                    var ctx = new ForEachPairMutableContext<T>(
                        source, i , j,
                        (index, replaceWith) =>
                        {
                            source[index] = replaceWith;
                            indicesToCompare[index] = true;
                            if (index == i)
                            {
                                lhsHasChanged = true;
                            }
                        },
                        index =>
                        {
                            source.RemoveAt(index);
                            indicesToCompare.RemoveAt(index);
                            lhsHasChanged = true;
                            nextIndexToCompare--;
                        });

                    callback(ctx);
                }
            }
        }
        
        private static bool MoveToNextIndexToCompare(IReadOnlyList<bool> indices, ref int currentIndex)
        {
            var nextIndex = (currentIndex + 1) % indices.Count;
            while (nextIndex != currentIndex)
            {
                if (indices[nextIndex])
                {
                    currentIndex = nextIndex;
                    return true;
                }

                nextIndex = (nextIndex + 1) % indices.Count;
            }

            currentIndex = -1;
            return false;
        }
    }
}