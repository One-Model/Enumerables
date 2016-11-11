using Xunit;

namespace OneModel.Enumerables.UnitTests.IEnumerable
{
    public class ConcatExtensionsTests
    {
        [Fact]
        public void Zero_Items_Can_Be_Appended()
        {
            var actual = new[] { 0, 1 }.Concat();

            Assert.Collection(actual,
                i => Assert.Equal(0, i),
                i => Assert.Equal(1, i));
        }

        [Fact]
        public void One_Item_Can_Be_Appended()
        {
            var actual = new[] { 0, 1 }.Concat(2);

            Assert.Collection(actual,
                i => Assert.Equal(0, i),
                i => Assert.Equal(1, i),
                i => Assert.Equal(2, i));
        }

        [Fact]
        public void Multiple_Item_Can_Be_Appended()
        {
            var actual = new[] { 0, 1 }.Concat(2, 3, 4, 5);

            Assert.Collection(actual,
                i => Assert.Equal(0, i),
                i => Assert.Equal(1, i),
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i),
                i => Assert.Equal(4, i),
                i => Assert.Equal(5, i));
        }
    }
}