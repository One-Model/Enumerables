using System.Collections.Generic;
using OneModel.Enumerables.IEnumerable;
using Xunit;

namespace OneModel.Enumerables.Tests.IEnumerable
{
    public class IsNullOrEmptyExtensionsTests
    {
        [Fact]
        public void Returns_True_For_Null_Enumerable()
        {
            var nullEnumerable = GetNullEnumerable<int>();
            
            var actual = nullEnumerable.IsNullOrEmpty();

            Assert.True(actual);
        }

        [Fact]
        public void Returns_True_For_Empty_Enumerable()
        {
            IEnumerable<int> emptyEnumerable = new List<int>();

            var actual = emptyEnumerable.IsNullOrEmpty();

            Assert.True(actual);
        }
        
        [Fact]
        public void Returns_False_For_Enumerable_With_Elements()
        {
            IEnumerable<int> nonEmptyNonNullEnurable = new List<int> { 1, 2, 3 };

            var actual = nonEmptyNonNullEnurable.IsNullOrEmpty();

            Assert.False(actual);
        }
        
        private static IEnumerable<T> GetNullEnumerable<T>()
        {
            return null;
        }
    }
}
