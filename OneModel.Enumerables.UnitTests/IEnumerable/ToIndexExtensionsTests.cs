using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OneModel.Enumerables.UnitTests.IEnumerable
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
    }
}
