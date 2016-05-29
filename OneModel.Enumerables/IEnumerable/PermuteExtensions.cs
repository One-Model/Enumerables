using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Calculates an enumeration of all of the permutations of a collection of collections, 
        /// where one item is picked from each source enumeration.
        /// </summary>
        public static IEnumerable<T[]> Permute<T>(this IEnumerable<IEnumerable<T>> sourceEnumerations)
        {
            if (sourceEnumerations == null) throw new ArgumentNullException(nameof(sourceEnumerations));

            var source = sourceEnumerations.Select(s => s.ToArray()).ToArray();

            var sourceLengths = source.Select(s => s.Length).ToArray();
            var sourceIndices = source.Select(s => 0).ToArray();
            var currentSource = source.Length - 1;

            yield return sourceIndices.Select((p, i) => source[i][p]).ToArray();

            while (PermuteMoveNext(sourceLengths, sourceIndices, currentSource))
            {
                yield return sourceIndices.Select((p, i) => source[i][p]).ToArray();
            }
        }

        /// <summary>
        /// Updates the indices to the next valid position.
        /// </summary>
        private static bool PermuteMoveNext(int[] lengths, int[] indices, int current)
        {
            if (current < 0)
            {
                return false;
            }

            indices[current]++;

            if (indices[current] < lengths[current])
            {
                return true;
            }

            indices[current] = 0;

            return PermuteMoveNext(lengths, indices, current - 1);
        }
    }
}
