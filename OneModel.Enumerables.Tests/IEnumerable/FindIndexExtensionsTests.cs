using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.Tests.IEnumerable
{
    public class FindIndexExtensionsTests
    {
        [Fact]
        public void Returns_Negative_1_When_There_Is_No_Match()
        {
            var enumerable = new List<int> { 1, 1, 1 };

            var actual = enumerable.FindIndex(item => item == 2);

            var expected = -1;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Returns_First_Index_Of_Predicate_Match()
        {
            var enumerable = new List<int> { 1, 2, 1, 2 };

            var actual = enumerable.FindIndex(item => item == 2);

            var expected = 1;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Returns_First_Index_Of_Predicate_Match_After_Start_Index()
        {
            var enumerable = new List<int> { 1, 2, 1, 2 };

            var actual = enumerable.FindIndex(2, item => item == 2);

            var expected = 3;
            Assert.Equal(expected, actual);
        }
    }
}
