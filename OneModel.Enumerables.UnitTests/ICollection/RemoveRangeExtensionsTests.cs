using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.UnitTests.ICollection
{
    public class RemoveRangeExtensionsTests
    {
        [Fact]
        public void RemoveRange_Removes_All_Items_From_Collection()
        {
            var collection = new List<int> { 1, 2 };
            var items = new List<int> { 1, 2 };

            collection.RemoveRange(items);

            Assert.Empty(collection);
        }

        [Fact]
        public void RemoveRange_Params_Overload_Removes_All_Items_From_Collection()
        {
            var collection = new List<int> { 1, 2, 3 };

            collection.RemoveRange(1, 2, 3);

            Assert.Empty(collection);
        }
    }
}
