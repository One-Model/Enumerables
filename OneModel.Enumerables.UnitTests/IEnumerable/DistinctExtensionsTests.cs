using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OneModel.Enumerables.UnitTests.IEnumerable
{
    public class DistinctExtensionsTests
    {
        public class DistinctKeepFirst
        {
            [Fact]
            public void Keeps_The_First_Duplicate()
            {
                var input = new[] { 0, 2 };
                var actual = input.DistinctKeepFirst(i => i % 2);
                Assert.Collection(actual,
                    i => Assert.Equal(i, 0));
            }

            [Fact]
            public void Is_Lazy()
            {
                var input = CreateBrokenEnumerable();
                // ReSharper disable once IteratorMethodResultIsIgnored
                input.DistinctKeepFirst(i => i);

                // The test here is that input is not enumerated
                // unless actual is enumerated, so it's 
                // correct that actual is never enumerated.
                // All we want to do is test that no exception is
                // thrown.
            }

            private IEnumerable<int> CreateBrokenEnumerable(params int[] values)
            {
                foreach (var value in values)
                {
                    yield return value;
                }
                throw new Exception("Sequence ran out of elements.");
            }
        }

        public class DistinctKeepLast
        {
            [Fact]
            public void Keeps_The_Last_Duplicate()
            {
                var input = new[] { 0, 2 };

                var actual = input.DistinctKeepLast(i => i % 2);
                Assert.Collection(actual,
                    i => Assert.Equal(i, 2));
            }
        }

        public class DistinctThrowExceptionTests
        {
            [Fact]
            public void Throws_Exception_On_Dupe()
            {
                var input = new[] { 1, 1 };
                Assert.ThrowsAny<Exception>(() => input.DistinctThrowException(i => i).ToList());
            }

            [Fact]
            public void Doesnt_Throw_Exception_If_No_Duplicates()
            {
                var input = new[] { 1, 2, 3 };
                var actual = input.DistinctThrowException(i => i);

                Assert.Collection(actual,
                    i => Assert.Equal(1, i),
                    i => Assert.Equal(2, i),
                    i => Assert.Equal(3, i));
            }

            [Fact]
            public void Returns_An_Empty_Enumeration_On_Empty_Input()
            {
                var input = new int[0];
                var actual = input.DistinctThrowException(i => i);
                Assert.Empty(actual);
            }

            [Fact]
            public void Items_Are_Matched_Based_On_Key_Selector_Fn()
            {
                var input = new[] { 2, 4 };
                Assert.ThrowsAny<Exception>(() => input.DistinctThrowException(i => i % 2).ToList());
            }
        }
    }
}
