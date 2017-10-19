using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.Tests.IEnumerable
{
    public class OrderByIdsExtensionsTests
    {
        [Fact]
        public void OrderByIds_Returns_An_Empty_Enumerable_When_Ids_Are_Null()
        {
            var enumerable = new List<int> { 1, 2, 3, 4, 5 };
            
            var actual = enumerable.OrderByIds((List<int>) null, (item, id) => item == id);
            
            Assert.Empty(actual);
        }

        [Fact]
        public void OrderByIds_Returns_Items_In_Order_By_Id()
        {
            var enumerable = new List<int> { 1, 2, 3, 4, 5 };
            var ids = new List<int> { 3, 2, 1 };

            var actual = enumerable.OrderByIds(ids, (item, id) => item == id);

            var expected = new List<int> { 3, 2, 1 };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByIds_Returns_Duplicates_When_Not_Distinct()
        {
            var enumerable = new List<int> { 1, 2, 3, 4, 5 };
            var ids = new List<int> { 3, 3 };

            var actual = enumerable.OrderByIds(ids, (item, id) => item == id);

            var expected = new List<int> { 3, 3 };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByIds_Does_Not_Return_Duplicates_When_Distinct()
        {
            var enumerable = new List<int> { 1, 2, 3, 4, 5 };
            var ids = new List<int> { 3, 3 };

            var actual = enumerable.OrderByIds(ids, (item, id) => item == id, true);

            var expected = new List<int> { 3 };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByIds_Overload_Returns_Items_With_Equal_Ids()
        {
            var item1 = new ItemWithId(1);
            var item2 = new ItemWithId(2);
            var item3 = new ItemWithId(3);
            var enumerable = new List<ItemWithId> { item1, item2, item3 };
            var ids = new List<int> { 1, 2 };

            var actual = enumerable.OrderByIds(ids, item => item.Id);

            var expected = new List<ItemWithId> { item1, item2 };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByIds_Overload_Returns_Items_That_Match_Predicate()
        {
            var item1 = new ItemWithId(1);
            var item2 = new ItemWithId(2);
            var item3 = new ItemWithId(3);
            var enumerable = new List<ItemWithId> { item1, item2, item3 };
            var ids = new List<int> { 1, 2 };

            var actual = enumerable.OrderByIds(ids, item => item.Id, (itemId, id) => itemId == id);

            var expected = new List<ItemWithId> { item1, item2 };
            Assert.Equal(expected, actual);
        }

        private class ItemWithId
        {
            public ItemWithId(int id)
            {
                Id = id;
            }

            public int Id { get; }
        }
    }
}
