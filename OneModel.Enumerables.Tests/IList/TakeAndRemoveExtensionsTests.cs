using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OneModel.Enumerables.Tests.IList
{
    public class TakeAndRemoveExtensionsTests
    {
        [Fact]
        public void TakeAndRemove_Returns_Null_When_Predicate_Finds_No_Match()
        {
            var a = Make(1);
            var b = Make(2);
            var c = Make(3);

            var input = new[] { a, b, c };

            var d = Make(4);

            var result = input.TakeAndRemove(item => item.Item == d.Item);

            Assert.Null(result);
        }

        [Fact]
        public void TakeAndRemove_Removes_Item_From_List_With_Matcher()
        {
            var a = Make(1);
            var b = Make(2);
            var c = Make(3);

            var input = new List<TestObject<int>> { a, b, c };

            input.TakeAndRemove(item => item.Item == c.Item);

            Assert.True(input.All(i => i.Item != c.Item));
        }

        [Fact]
        public void TakeAndRemove_Returns_Item_From_List_On_Match()
        {
            var a = Make(1);
            var b = Make(2);
            var c = Make(3);

            var input = new List<TestObject<int>> { a, b, c };

            var result = input.TakeAndRemove(item => item.Item == c.Item);

            Assert.Equal(3, result.Item);
        }

        [Fact]
        public void TakeAndRemove_Removes_Item_From_List()
        {
            var a = Make(1);
            var b = Make(2);
            var c = Make(3);

            var input = new List<TestObject<int>> { a, b, c };

            input.TakeAndRemove(c);

            Assert.True(input.All(i => i.Item != c.Item));
        }


        [Fact]
        public void TakeAndRemove_Returns_Empty_When_Item_Not_In_List()
        {
            var a = Make(1);
            var b = Make(2);
            var c = Make(3);

            var input = new List<TestObject<int>> { a, b, c };

            var d = Make(4);

            var result = input.TakeAndRemove(d);

            Assert.Null(result);
        }

        private TestObject<T> Make<T>(T item) where T : IComparable
        {
            return new TestObject<T>
            {
                Item = item
            };
        }

        private class TestObject<T> where T : IComparable
        {
            public T Item;
        }
    }
}
