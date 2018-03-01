using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace OneModel.Enumerables.Tests.IEnumerable
{
    public class SplitExtensionTests
    {
        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void Throws_Exception_If_Source_Is_Null()
        {
            IEnumerable<string> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Split(i => i == "").ToArray());
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void Throws_Exception_If_Predicate_Is_Null()
        {
            IEnumerable<string> source = new string[0];
            Assert.Throws<ArgumentNullException>(() => source.Split(null).ToArray());
        }

        [Theory]
        [MemberData(nameof(Cases))]
        public void Returns_Expected_Values(IEnumerable<string> input, IReadOnlyCollection<IReadOnlyList<string>> expected)
        {
            var actual = input.Split(i => i == "");
            Assert.Equal(expected, actual, new ListComparer());
        }

        public static IEnumerable<object[]> Cases()
        {
            yield return new object[]
            {
                new string[]
                {
                    null
                },
                new List<List<string>>
                {
                    new List<string>
                    {
                        null
                    }
                }
            };

            yield return new object[]
            {
                new string[0],
                new List<List<string>>
                {
                    new List<string>()
                }
            };

            yield return new object[]
            {
                new []{ "" },
                new List<List<string>>
                {
                    new List<string>(),
                    new List<string>()
                }
            };

            yield return new object[]
            {
                new []{ "a" },
                new List<List<string>>
                {
                    new List<string>
                    {
                        "a"
                    }
                }
            };

            yield return new object[]
            {
                new []{ "a", "b" },
                new List<List<string>>
                {
                    new List<string>
                    {
                        "a",
                        "b"
                    }
                }
            };

            yield return new object[]
            {
                new []{ "", "a", "b" },
                new List<List<string>>
                {
                    new List<string>(),
                    new List<string>
                    {
                        "a",
                        "b"
                    }
                }
            };

            yield return new object[]
            {
                new []{ "", "" },
                new List<List<string>>
                {
                    new List<string>(),
                    new List<string>(),
                    new List<string>()
                }
            };

            yield return new object[]
            {
                new []{ "", "a", "" },
                new List<List<string>>
                {
                    new List<string>(),
                    new List<string>
                    {
                        "a"
                    },
                    new List<string>()
                }
            };

            yield return new object[]
            {
                new []{ "", "a", "b", "" },
                new List<List<string>>
                {
                    new List<string>(),
                    new List<string>
                    {
                        "a",
                        "b"
                    },
                    new List<string>()
                }
            };

            yield return new object[]
            {
                new []{ "", "a", "b", "", "c" },
                new List<List<string>>
                {
                    new List<string>(),
                    new List<string>
                    {
                        "a",
                        "b"
                    },
                    new List<string>
                    {
                        "c"
                    }
                }
            };
        }

        private class ListComparer : IEqualityComparer<IReadOnlyList<string>>
        {
            public bool Equals(IReadOnlyList<string> x, IReadOnlyList<string> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(IReadOnlyList<string> obj)
            {
                throw new NotSupportedException();
            }
        }
    }
}