using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.Tests.IEnumerable
{
    public class ToExtensionsTests
    {
        [Fact]
        public void ToHashSet_Returns_Enumerable_As_HashSet()
        {
            var collection = new List<int> { 1, 2, 3 };

            var actual = collection.ToHashSet();

            var expected = new HashSet<int> { 1, 2, 3 };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToLinkedList_Returns_Enumerable_As_LinkedList()
        {
            var collection = new List<int> { 1, 2, 3 };

            var actual = collection.ToLinkedList();

            var expected = new LinkedList<int>();
            var firstNode = expected.AddFirst(1);
            var secondNode = expected.AddAfter(firstNode, 2);
            expected.AddAfter(secondNode, 3);

            Assert.Equal(expected, actual);
        }
    }
}
