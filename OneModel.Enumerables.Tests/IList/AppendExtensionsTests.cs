using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace OneModel.Enumerables.Tests.IList
{
    public class AppendExtensionsTests
    {
        [Fact]
        public void Append_Throws_Exception_If_Source_Is_Null()
        {
            List<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Append(0));
        }

        [Fact]
        public void Append_Returns_The_Source_list()
        {
            var source = new List<int>();
            var actual = source.Append(0);
            
            Assert.Same(source, actual);
        }
        
        [Fact]
        public void Append_Inserts_The_Item_At_The_End()
        {
            var source = new List<int> { 0, 1 };
            source.Append(2);

            Assert.Collection(source, 
                item => Assert.Equal(0, item),
                item => Assert.Equal(1, item),
                item => Assert.Equal(2, item));
        }
        
        [Fact]
        public void AppendRange_Array_Throws_Exception_If_Source_Is_Null()
        {
            List<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.AppendRange(0, 1));
        }

        [Fact]
        public void AppendRange_Array_Throws_Exception_If_Items_Array_Is_Null()
        {
            List<int> source = new List<int>();
            Assert.Throws<ArgumentNullException>(() => source.AppendRange((IEnumerable<int>)null));
        }

        [Fact]
        public void AppendRange_Array_Inserts_The_Items_At_The_End()
        {
            var source = new List<int> { 0, 1 };
            source.AppendRange(2, 3);

            Assert.Collection(source,
                item => Assert.Equal(0, item),
                item => Assert.Equal(1, item),
                item => Assert.Equal(2, item),
                item => Assert.Equal(3, item));
        }
        
        [Fact]
        public void AppendRange_Enumerable_Throws_Exception_If_Source_Is_Null()
        {
            List<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.AppendRange(new List<int> { 0, 1 }));
        }

        [Fact]
        public void AppendRange_Enumerable_Throws_Exception_If_Items_Array_Is_Null()
        {
            List<int> source = new List<int>();
            Assert.Throws<ArgumentNullException>(() => source.AppendRange((IEnumerable<int>)null));
        }

        [Fact]
        public void AppendRange_Enumerable_Inserts_The_Items_At_The_End()
        {
            var source = new List<int> { 0, 1 };
            source.AppendRange(new[] { 2, 3 });

            Assert.Collection(source,
                item => Assert.Equal(0, item),
                item => Assert.Equal(1, item),
                item => Assert.Equal(2, item),
                item => Assert.Equal(3, item));
        }
        
        private class MockList<T> : IList<T>
        {
            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(T item)
            {
            }

            public void Clear()
            {
            }

            public bool Contains(T item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
            }

            public bool Remove(T item)
            {
                throw new NotImplementedException();
            }

            public int Count { get; }
            public bool IsReadOnly { get; }
            public int IndexOf(T item)
            {
                throw new NotImplementedException();
            }

            public void Insert(int index, T item)
            {
            }

            public void RemoveAt(int index)
            {
            }

            public T this[int index]
            {
                get { throw new NotImplementedException(); }
                set { throw new NotImplementedException(); }
            }
        }
    }
}
