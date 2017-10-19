using System;
using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.Tests.IDictionary
{
    public class DictionaryEqualExtensionsTests
    {
        [Fact]
        public void Throws_Exception_If_Source_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Dictionary<int, string> source = null;
                var other = new Dictionary<int, string>();
                // ReSharper disable once ExpressionIsAlwaysNull
                source.DictionaryEquals(other);
            });
        }

        [Fact]
        public void Throws_Exception_If_Other_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var source = new Dictionary<int, string>();
                Dictionary<int, string> other = null;
                // ReSharper disable once ExpressionIsAlwaysNull
                source.DictionaryEquals(other);
            });
        }

        [Fact]
        public void Returns_True_For_Empty_Dictionaries()
        {
            var source = new Dictionary<int, string>();
            var other = new Dictionary<int, string>();
            Assert.True(source.DictionaryEquals(other));
        }

        [Theory]
        [MemberData(nameof(Returns_Expected_Values_Cases))]
        public void Returns_Expected_Values(bool expected, IReadOnlyDictionary<int, string> source, IReadOnlyDictionary<int, string> other)
        {
            var actual = source.DictionaryEquals(other);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> Returns_Expected_Values_Cases()
        {
            return new[]
            {
                new object[]
                {
                    true,
                    new Dictionary<int, string>(), 
                    new Dictionary<int, string>()
                }, 
                new object[]
                {
                    true,
                    new Dictionary<int, string>
                    {
                        { 1, "a" }
                    },
                    new Dictionary<int, string>
                    {
                        { 1, "a" }
                    }
                }, 
                new object[]
                {
                    true,
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { 2, "b" }
                    },
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { 2, "b" }
                    }
                }, 
                new object[]
                {
                    false,
                    new Dictionary<int, string>
                    {
                        { 1, "a" }
                    },
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { 2, "b" }
                    }
                },
                new object[]
                {
                    false,
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { 2, "b" }
                    },
                    new Dictionary<int, string>
                    {
                        { 1, "a" }
                    }
                },
                new object[]
                {
                    false,
                    new Dictionary<int, string>(),
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { 2, "b" }
                    }
                },
                new object[]
                {
                    false,
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { 2, "b" }
                    },
                    new Dictionary<int, string>()
                },
                new object[]
                {
                    false,
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { 2, "b" }
                    },
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { 2, "X" }
                    },
                },
                new object[]
                {
                    false,
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { 2, "b" }
                    },
                    new Dictionary<int, string>
                    {
                        { 1, "a" },
                        { -2, "b" }
                    },
                }
            };
        }

        [Theory]
        [MemberData(nameof(Returns_Expected_Values_With_Comparer_Cases))]
        public void Returns_Expected_Values_With_Comparer(bool expected, IEqualityComparer<int> valueComparer, IReadOnlyDictionary<int, int> source, IReadOnlyDictionary<int, int> other)
        {
            var actual = source.DictionaryEquals(other, valueComparer);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> Returns_Expected_Values_With_Comparer_Cases()
        {
            return new[]
            {
                new object[]
                {
                    true,
                    new ModuloComparer(3),
                    new Dictionary<int, int>(),
                    new Dictionary<int, int>()
                },
                new object[]
                {
                    true,
                    new ModuloComparer(3),
                    new Dictionary<int, int>
                    {
                        { 1, 2 }
                    },
                    new Dictionary<int, int>
                    {
                        { 1, 2 }
                    }
                },
                new object[]
                {
                    true,
                    new ModuloComparer(3),
                    new Dictionary<int, int>
                    {
                        { 1, 3 }
                    },
                    new Dictionary<int, int>
                    {
                        { 1, 0 }
                    }
                },
                new object[]
                {
                    true,
                    new ModuloComparer(3),
                    new Dictionary<int, int>
                    {
                        { 1, 3 },
                        { 2, 5 }
                    },
                    new Dictionary<int, int>
                    {
                        { 1, 0 },
                        { 2, 2 }
                    }
                }
            };
        }

        private class ModuloComparer : IEqualityComparer<int>
        {
            private readonly int _divisor;

            public ModuloComparer(int divisor)
            {
                _divisor = divisor;
            }

            public bool Equals(int x, int y)
            {
                return x % _divisor == y % _divisor;
            }

            public int GetHashCode(int obj)
            {
                return obj;
            }
        }
    }
}