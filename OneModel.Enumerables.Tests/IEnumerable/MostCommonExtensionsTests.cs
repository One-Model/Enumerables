using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.Tests.IEnumerable
{
    public class MostCommonExtensionsTests
    {
        [Theory, MemberData(nameof(TestData))]
        public void MostCommon_Returns_Expected_Results(IEnumerable<int> enumerable, int expected)
        {
            var actual = enumerable.MostCommon();

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> TestData => new[]
        {
            new object[]
            {
                new[] { 1, 1, 1, 2, 2 },
                1
            },

            new object[]
            {
                new[] { 1, 1, 2, 2 },
                1
            },

            new object[]
            {
                new[] { 1 },
                1
            }
        };
    }
}
