using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.UnitTests.IEnumerable
{
    public class VennExtensionsTests
    {
        [Fact]
        public void Venn_Returns_Expected_Results()
        {
            var left = new List<int> { 1, 2 };
            var right = new List<int> { 2, 3 };

            var actual = left.Venn(right, leftItem => leftItem, rightItem => rightItem);

            var expected = new VennResult<int, int>(
                new List<int> { 1 },
                new List<int> { 3 },
                new List<VennResult<int, int>.Pair>
                {
                    new VennResult<int, int>.Pair(2, 2)
                });

            AssertVennResultsEqual(expected, actual);
        }

        [Fact]
        public void AssertVennResultsEqual_Passes_With_Equivalent_Objects()
        {
            var result1 = new VennResult<int, int>(
                new List<int> { 1 },
                new List<int> { 3 },
                new List<VennResult<int, int>.Pair>
                {
                    new VennResult<int, int>.Pair(2, 2)
                });

            var result2 = new VennResult<int, int>(
                new List<int> { 1 },
                new List<int> { 3 },
                new List<VennResult<int, int>.Pair>
                {
                    new VennResult<int, int>.Pair(2, 2)
                });

            AssertVennResultsEqual(result1, result2);
        }

        private void AssertVennResultsEqual<TLeft, TRight>(
            VennResult<TLeft, TRight> expected,
            VennResult<TLeft, TRight> actual)
        {
            Assert.Equal(expected.Left, actual.Left);
            Assert.Equal(expected.Right, actual.Right);

            Assert.Equal(expected.Both.Count, actual.Both.Count);
            for (int i = 0; i < expected.Both.Count; i++)
            {
                Assert.Equal(expected.Both[i], actual.Both[i]);
            }
        }
    }
}
