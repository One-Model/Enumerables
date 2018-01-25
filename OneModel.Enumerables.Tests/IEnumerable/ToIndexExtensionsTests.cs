using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OneModel.Enumerables.Tests.IEnumerable
{
    public class ToIndexTests
    {
        public class MockSingle
        {
            public MockSingle(string value, int key)
            {
                Value = value;
                Key = key;
            }

            public string Value { get; }

            public int Key { get; }
        }

        public class MockMulti
        {
            public MockMulti(string value, params int[] keys)
            {
                Value = value;
                Keys = keys;
            }

            public string Value { get; }

            public int[] Keys { get; }
        }

        [Fact]
        public void A_Single_Key_Can_Be_Extracted_From_Objects()
        {
            var input = new List<MockSingle>
            {
                new MockSingle("a", 1),
                new MockSingle("b", 2)
            };

            var index = input.ToIndex(item => item.Key);
            var keys = index.Keys.OrderBy(i => i);

            Assert.Collection(keys,
                item => Assert.Equal(1, item),
                item => Assert.Equal(2, item));
        }

        [Fact]
        public void Multiple_Keys_Can_Be_Extracted_From_Objects()
        {
            var input = new List<MockMulti>
            {
                new MockMulti("a", 1, 2),
                new MockMulti("b", 1)
            };

            var index = input.ToIndex<int, MockMulti>(item => item.Keys);
            var keys = index.Keys.OrderBy(i => i);

            Assert.Collection(keys,
                item => Assert.Equal(1, item),
                item => Assert.Equal(2, item));
        }

        [Fact]
        public void Items_Can_Be_Placed_Into_Multiple_Collections()
        {
            var input = new List<MockMulti>
            {
                new MockMulti("a", 1, 2)
            };

            var index = input.ToIndex<int, MockMulti>(item => item.Keys);

            Assert.Contains(index[1], item => item.Value == "a");
            Assert.Contains(index[2], item => item.Value == "a");
        }

        [Fact]
        public void A_Key_Comparer_Can_Be_Supplied()
        {
            var input = new List<MockSingle>
            {
                new MockSingle("a", 1),
                new MockSingle("b", 2),
                new MockSingle("c", 3),
                new MockSingle("d", 4),
                new MockSingle("e", 5),
                new MockSingle("f", 6)
            };

            var index = input.ToIndex(
                item => item.Key,
                item => item.Value,
                new ModComparer()
            );
            
            Assert.Collection(index.Collections,
                i =>
                {
                    Assert.Equal(1, i.Key);
                    Assert.Equal(new[]{ "a", "c", "e" }, i.Value);
                },
                i =>
                {
                    Assert.Equal(2, i.Key);
                    Assert.Equal(new[]{ "b", "d", "f" }, i.Value);
                }
            );
        }

        private class ModComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return x % 2 == y % 2;
            }

            public int GetHashCode(int obj)
            {
                return obj % 2;
            }
        }
    }
}