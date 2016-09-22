using System;
using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.UnitTests.IEnumerable
{
    public class ComputeHashCodeExtensionsTests
    {
        [Fact]
        public void ComputedHashCode_Throws_Exception_If_Source_Is_Null()
        {
            int[] input = null;
            Assert.Throws<ArgumentNullException>(() => input.ComputeHashCode());
        }

        [Fact]
        public void ComputedHashCode_Can_Handle_An_Empty_Enumerable()
        {
            var input = new int[0];
            // No assert required - just testing that it doesn't throw an exception.
            input.ComputeHashCode();
        }

        [Theory]
        [MemberData(nameof(ComputedHashCode_Produces_Different_Hashes_For_Different_Enumerables_Data))]
        public void ComputedHashCode_Produces_Different_Hashes_For_Different_Enumerables(object[] a, object[] b)
        {
            var hash1 = a.ComputeHashCode();
            var hash2 = b.ComputeHashCode();

            Assert.NotEqual(hash1, hash2);
        }
        
        public static IEnumerable<object[]> ComputedHashCode_Produces_Different_Hashes_For_Different_Enumerables_Data
        {
            get
            {
                yield return new object[]
                {
                    new object[] { },
                    new object[] { 1 }
                };

                yield return new object[]
                {
                    new object[] { 0, 1, 2 },
                    new object[] { 0, 1, 2, 3 }
                };

                yield return new object[]
                {
                    new object[] { 0, 1 },
                    new object[] { 1, 0 }
                };

                yield return new object[]
                {
                    new object[] { 0 },
                    new object[] { "" }
                };
            }
        }

        [Theory]
        [MemberData(nameof(ComputedHashCode_Produces_The_Same_Hashes_For_Similar_Enumerables_Data))]
        public void ComputedHashCode_Produces_The_Same_Hashes_For_Similar_Enumerables(object[] a, object[] b)
        {
            var hash1 = a.ComputeHashCode();
            var hash2 = b.ComputeHashCode();

            Assert.Equal(hash1, hash2);
        }

        public static IEnumerable<object[]> ComputedHashCode_Produces_The_Same_Hashes_For_Similar_Enumerables_Data
        {
            get
            {
                yield return new object[]
                {
                    new object[] { },
                    new object[] { }
                };

                yield return new object[]
                {
                    new object[] { 0 },
                    new object[] { 0 }
                };

                yield return new object[]
                {
                    new object[] { 0, 1, 2 },
                    new object[] { 0, 1, 2 }
                };

                yield return new object[]
                {
                    new object[] { "a" },
                    new object[] { "a" }
                };
            }
        }

    }
}
