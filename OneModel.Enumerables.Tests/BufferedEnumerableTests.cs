using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OneModel.Enumerables.Tests
{
    public class BufferedEnumerableTests
    {
        [Fact]
        public void Results_Are_Buffered()
        {
            var sequence = new Sequence(0, 3);
            var buffered = sequence.Buffer();
            var bufferedA = buffered.ToArray();
            var bufferedB = buffered.ToArray();

            var sequenceA = sequence.ToArray();
            var sequenceB = sequence.ToArray();

            Assert.True(bufferedA.SequenceEqual(bufferedB));
            Assert.False(sequenceA.SequenceEqual(sequenceB));
        }

        /// <summary>
        /// An enumerable that returns different results on each enumeration.
        /// </summary>
        private class Sequence : IEnumerable<int>
        {
            private int _from;
            private readonly int _length;

            public Sequence(int from, int length)
            {
                _from = from;
                _length = length;
            }

            public IEnumerator<int> GetEnumerator()
            {
                for (var i = 0; i < _length; i++)
                {
                    yield return _from++;
                }
            }

            IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}