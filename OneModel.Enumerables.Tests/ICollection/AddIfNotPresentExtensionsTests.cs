using System.Collections.Generic;
using System.Linq;
using OneModel.Enumerables.ICollection;
using Xunit;

namespace OneModel.Enumerables.Tests.ICollection
{
    public class AddIfNotPresentExtensionsTests
    {
        public static IEnumerable<object[]> Cases
        {
            get
            {
                yield return new object[]
                {
                        new List<int>(),
                        0,
                        new List<int> { 0 }
                };

                yield return new object[]
                {
                        new List<int> { 0, 1 },
                        2,
                        new List<int> { 0, 1, 2 }
                };

                yield return new object[]
                {
                        new List<int> { 0, 1, 2 },
                        2,
                        new List<int> { 0, 1, 2 }
                };

                yield return new object[]
                {
                        new List<int> { 0, 1, 2 },
                        12,
                        new List<int> { 0, 1, 2 }
                };

                yield return new object[]
                {
                        new List<int> { 0, 1, 2 },
                        13,
                        new List<int> { 0, 1, 2, 13 }
                };
            }
        }

        public class Default_Comparer
        {
            [Theory]
            [MemberData(nameof(Returns_Expected_Output_Cases))]
            public void Returns_Expected_Output(ICollection<int> source, int add, ICollection<int> expected)
            {
                source.AddIfNotPresent(add);
                Assert.True(source.SequenceEqual(expected));
            }

            // ReSharper disable once InconsistentNaming
            public static IEnumerable<object[]> Returns_Expected_Output_Cases
            {
                get
                {
                    yield return new object[]
                    {
                        new List<int>(),
                        0,
                        new List<int> { 0 }
                    };

                    yield return new object[]
                    {
                        new List<int> { 0, 1 },
                        2,
                        new List<int> { 0, 1, 2 }
                    };

                    yield return new object[]
                    {
                        new List<int> { 0, 1, 2 },
                        2,
                        new List<int> { 0, 1, 2 }
                    };
                }
            }
        }

        public class KeyFunction
        {
            [Theory]
            [MemberData(nameof(Returns_Expected_Output_Cases))]
            public void Returns_Expected_Output(ICollection<int> source, int add, ICollection<int> expected)
            {
                source.AddIfNotPresent(add, i => i % 10);
                Assert.True(source.SequenceEqual(expected));
            }

            // ReSharper disable once InconsistentNaming
            public static IEnumerable<object[]> Returns_Expected_Output_Cases => Cases;
        }

        public class Comparer : IEqualityComparer<int>
        {
            [Theory]
            [MemberData(nameof(Returns_Expected_Output_Cases))]
            public void Returns_Expected_Output(ICollection<int> source, int add, ICollection<int> expected)
            {
                source.AddIfNotPresent(add, this);
                Assert.True(source.SequenceEqual(expected));
            }

            // ReSharper disable once InconsistentNaming
            public static IEnumerable<object[]> Returns_Expected_Output_Cases => Cases;

            public bool Equals(int x, int y)
            {
                return x % 10 == y % 10;
            }

            public int GetHashCode(int obj)
            {
                return obj;
            }
        }

        public class FuncComparer
        {
            [Theory]
            [MemberData(nameof(Returns_Expected_Output_Cases))]
            public void Returns_Expected_Output(ICollection<int> source, int add, ICollection<int> expected)
            {
                source.AddIfNotPresent(add, (a,b) => a % 10 == b % 10);
                Assert.True(source.SequenceEqual(expected));
            }

            // ReSharper disable once InconsistentNaming
            public static IEnumerable<object[]> Returns_Expected_Output_Cases => Cases;

            public bool Equals(int x, int y)
            {
                return x == y;
            }

            public int GetHashCode(int obj)
            {
                return obj;
            }
        }
    }
}