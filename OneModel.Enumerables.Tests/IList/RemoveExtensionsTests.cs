using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.Tests.IList
{
    public class RemoveExtensionsTests
    {
        [Fact]
        public void Remove_Removes_All_Items_That_Match_Predicate()
        {
            var collection = new List<int> { 1, 1, 2, 1 };

            collection.Remove(item => item == 1);

            var expected = new List<int> { 2 };
            Assert.Equal(expected, collection);
        }

        [Fact]
        public void Remove_Overload_Removes_All_Items_That_Match_Predicate()
        {
            var collection = new List<int> { 1, 1, 2, 1 };

            collection.Remove((item, index) => item == 1);

            var expected = new List<int> { 2 };
            Assert.Equal(expected, collection);
        }

        [Fact]
        public void Remove_Overload_Provides_Index_To_Predicate_Function()
        {
            var collection = new List<int> { 1, 1, 2, 1 };
            var indexes = new List<int>();

            collection.Remove((item, index) =>
            {
                indexes.Add(index);
                return item == 1;
            });

            var expectedIndexes = new List<int> { 3, 2, 1, 0 };
            Assert.Equal(expectedIndexes, indexes);
        }
    }
}
