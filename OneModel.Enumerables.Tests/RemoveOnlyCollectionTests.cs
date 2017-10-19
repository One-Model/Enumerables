using System;
using System.Linq;
using Xunit;

namespace OneModel.Enumerables.Tests
{
    public class RemoveOnlyCollectionTests
    {
        [Fact]
        public void Throws_Exception_Is_Items_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new FastRemoveList<int>(null));
        }
        
        [Fact]
        public void Items_Can_Be_Enumerated()
        {
            var collection = new FastRemoveList<int>(1, 2, 3);
            Assert.Collection(collection.OrderBy(a => a),
                i => Assert.Equal(1, i),
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i));
        }

        [Fact]
        public void Items_Can_Be_Removed_From_The_End()
        {
            var collection = new FastRemoveList<int>(1, 2, 3);
            collection.RemoveAt(2);
            
            Assert.Collection(collection,
                i => Assert.Equal(1, i),
                i => Assert.Equal(2, i));
        }

        [Fact]
        public void Items_Can_Be_Removed_From_The_Start()
        {
            var collection = new FastRemoveList<int>(1, 2, 3);
            collection.RemoveAt(0);
            Assert.Collection(collection.OrderBy(a => a),
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i));
        }
        
        [Fact]
        public void All_Items_Can_Be_Removed()
        {
            var collection = new FastRemoveList<int>(1, 2, 3);
            collection.RemoveAt(0);
            collection.RemoveAt(0);
            collection.RemoveAt(0);
            Assert.Empty(collection);
        }

        [Fact]
        public void Items_Can_Be_Retrieved_By_Index()
        {
            var collection = new FastRemoveList<int>(1, 2, 3);
            Assert.Equal(1, collection[0]);
            Assert.Equal(2, collection[1]);
            Assert.Equal(3, collection[2]);
        }

        [Fact]
        public void Items_Can_Be_Retrieved_By_Index_After_Removing_Items()
        {
            var collection = new FastRemoveList<int>(1, 2, 3);
            collection.RemoveAt(0);
            Assert.Equal(3, collection[0]);
            Assert.Equal(2, collection[1]);
        }
    }
}