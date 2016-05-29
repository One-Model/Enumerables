using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.UnitTests.ICollection
{
    public class AddRangeExtensionsTests
    {
        [Fact]
        public void AddRange_Adds_All_Items_To_Collection()
        {
            // Using HashSet, because List<T> has its own AddRange method.
            var collection = new HashSet<int>();
            var items = new List<int> { 1, 2 };

            collection.AddRange(items);

            Assert.Collection(
                collection,
                item => Assert.Equal(1, item),
                item => Assert.Equal(2, item));
        }

        [Fact]
        public void AddRange_Params_Overload_Adds_All_Items_To_Collection()
        {
            // Using HashSet, because List<T> has its own AddRange method.
            var collection = new HashSet<int>();

            collection.AddRange(1, 2);

            Assert.Collection(
                collection,
                item => Assert.Equal(1, item),
                item => Assert.Equal(2, item));
        }
    }
}
