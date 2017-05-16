using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OneModel.Enumerables
{
    /// <summary>
    /// An implementation of IReadOnlyList that only permits
    /// reading and removing items. New items cannot be 
    /// added. Supports efficient O(1) removal of items
    /// like a linked list, but supports index-based 
    /// retrieval of items as well. Removing items will
    /// reorder the collection unless the item removed is 
    /// the last item.
    /// </summary>
    public class FastRemoveList<T> : IReadOnlyList<T>
    {
        public FastRemoveList(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            _items = items.ToArray();
            Count = _items.Length;
        }

        public FastRemoveList(params T[] items) : this((IEnumerable<T>)items)
        {
        }

        private readonly T[] _items;

        public int Count { get; private set; }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            CheckBounds(index);

            _items[index] = _items[Count - 1];
            Count--;
            _items[Count] = default(T);
        }

        private void CheckBounds(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Argument {nameof(index)} must not be less than 0.");
            }

            if (index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Argument {nameof(index)} was outside the bounds of the collection.");
            }
        }

        public T this[int index]
        {
            get
            {
                CheckBounds(index);
                return _items[index];
            }
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(_items, Count);
        }

        private class Enumerator : IEnumerator<T>
        {
            private readonly T[] _items;
            private readonly int _count;
            private int _position = -1;

            public Enumerator(T[] items, int count)
            {
                _items = items;
                _count = count;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return ++_position < _count;
            }

            public void Reset()
            {
                _position = -1;
            }

            object IEnumerator.Current => Current;

            public T Current => _items[_position];
        }
    }
}