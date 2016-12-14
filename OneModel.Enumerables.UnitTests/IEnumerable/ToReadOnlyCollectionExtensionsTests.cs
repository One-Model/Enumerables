using System.Collections.Generic;
using System.Linq;
using OneModel.Enumerables.IEnumerable;
using Xunit;

namespace OneModel.Enumerables.UnitTests.IEnumerable
{
    public class ToReadOnlyCollectionExtensionsTests
    {
        [Theory]
        [MemberData(nameof(Returns_Same_Object_If_The_Source_Is_An_IReadOnlyCollection_Cases))]
        public void Returns_Same_Object_If_The_Source_Is_An_IReadOnlyCollection(IEnumerable<int> source)
        {
            var actual = source.ToReadOnlyCollection();
            Assert.Same(source, actual);
        }

        public static IEnumerable<object[]> Returns_Same_Object_If_The_Source_Is_An_IReadOnlyCollection_Cases()
        {
            yield return new object[] 
            {
                new List<int> { 0, 1 }
            };

            yield return new object[]
            {
                new[] { 0, 1 }
            };

            yield return new object[]
            {
                new LinkedList<int>(new []{ 0, 1 }), 
            };
        }

        [Fact]
        public void Returns_Different_Object_If_The_Source_Is_Not_An_IReadOnlyCollection()
        {
            var source = Enumerable.Range(0, 2);
            var actual = source.ToReadOnlyCollection();
            Assert.NotSame(source, actual);
        }
    }
}