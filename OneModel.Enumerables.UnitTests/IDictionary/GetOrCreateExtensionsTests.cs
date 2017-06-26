using System;
using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.UnitTests.IDictionary
{
    public class GetOrCreateExtensionsTests
    {
        [Fact]
        public void Throws_Exception_If_Source_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Dictionary<int, string> source = null;
                // ReSharper disable once ExpressionIsAlwaysNull
                source.GetOrCreate(1, () => "a");
            });
        }

        [Fact]
        public void Throws_Exception_If_Creator_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var source = new Dictionary<int, string>();
                // ReSharper disable once ExpressionIsAlwaysNull
                source.GetOrCreate(1, null);
            });
        }

        [Fact]
        public void Does_Not_Create_New_Value_When_Existing_Value_Is_Found()
        {
            var oldInstance = new object();
            var newInstance = new object();
            var source = new Dictionary<int, object>
            {
                { 1, oldInstance }
            };

            var actual = source.GetOrCreate(1, () => newInstance);
            Assert.Same(oldInstance, actual);
            Assert.Same(oldInstance, source[1]);
        }

        [Fact]
        public void Creates_New_Value_If_Value_Not_Found_For_Key()
        {
            var oldInstance = new object();
            var newInstance = new object();
            var source = new Dictionary<int, object>
            {
                { 1, oldInstance }
            };

            var actual = source.GetOrCreate(2, () => newInstance);
            Assert.Same(newInstance, actual);
            Assert.Same(oldInstance, source[1]);
        }

    }
}