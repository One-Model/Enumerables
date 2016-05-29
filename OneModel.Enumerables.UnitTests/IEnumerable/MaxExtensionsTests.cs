using System;
using Xunit;

namespace OneModel.Enumerables.UnitTests.IEnumerable
{
    public class MaxExtensionsTests
    {
        public class TestObject<T> where T : IComparable
        {
            public T Order;
        }

        [Fact]
        public void Can_Compare_Integers()
        {
            var a = V(2);
            var b = V(4);
            var c = V(3);
            var input = new[] { a, b, c };
            var min = input.Max2(i => i.Order);

            Assert.Same(b, min);
        }

        [Fact]
        public void Can_Handle_Negative_Integers()
        {
            var a = V(2);
            var b = V(1);
            var c = V(3);
            var input = new[] { a, b, c };
            var min = input.Max2(i => i.Order);

            Assert.Same(c, min);
        }

        [Fact]
        public void Throws_Exception_On_Empty_List_If_ThrowException_Behavior_Specified()
        {
            var input = new TestObject<int>[0];
            Assert.Throws<ArgumentException>(() => input.Max2(i => i.Order, MinMaxEmptyBehavior.ThrowException));
        }

        [Fact]
        public void Returns_Default_Value_On_Empty_List_If_ReturnDefault_Behavior_Specified()
        {
            var input = new TestObject<int>[0];
            var min = input.Max2(i => i.Order);
            Assert.Same(null, min);
        }

        [Fact]
        public void Can_Compare_Floats()
        {
            var a = V(2f);
            var b = V(1f);
            var c = V(-3f);
            var input = new[] { a, b, c };
            var min = input.Max2(i => i.Order);

            Assert.Same(a, min);
        }

        /// <summary>
        /// Builds a test object. For convenience.
        /// </summary>
        private TestObject<T> V<T>(T order) where T : IComparable
        {
            return new TestObject<T>
            {
                Order = order
            };
        }
    }
}
