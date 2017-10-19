using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.Tests
{
    public class VennResultTests
    {
        public class PairTests
        {
            [Theory, MemberData(nameof(TestData))]
            public void Equals_Returns_Expected_Result(
                VennResult<int, int>.Pair pair1,
                VennResult<int, int>.Pair pair2,
                bool expected)
            {
                Assert.Equal(expected, pair1.Equals(pair2));
            }

            public static IEnumerable<object[]> TestData => new[]
            {
                new object[]
                {
                    new VennResult<int, int>.Pair(2, 2),
                    new VennResult<int, int>.Pair(2, 2),
                    true
                },

                new object[]
                {
                    new VennResult<int, int>.Pair(1, 2),
                    new VennResult<int, int>.Pair(2, 2),
                    false
                },

                new object[]
                {
                    new VennResult<int, int>.Pair(2, 1),
                    new VennResult<int, int>.Pair(2, 2),
                    false
                }
            };
        }
    }
}
