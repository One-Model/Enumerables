using System.Collections;
using System.Collections.Generic;

namespace OneModel.Enumerables
{
    /// <summary>
    /// Wraps an enumerable, allowing it to be read multiple
    /// times without enumerating the underlying source more
    /// than once, and also without enumerating the underlying
    /// source up front.
    /// </summary>
    public class BufferedEnumerable<T> : IEnumerable<T>
    {
        private readonly IList<T> _buffer;
        private readonly IEnumerator<T> _source;
        
        public BufferedEnumerable(IEnumerable<T> source)
        {
            _source = source.GetEnumerator();
            _buffer = new List<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BufferedEnumerator<T>(_source, _buffer);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// The number of items that have been read into the buffer
        /// so far.
        /// </summary>
        public int NumRead => _buffer.Count;

        public class BufferedEnumerator<T> : IEnumerator<T>
        {
            private readonly IEnumerator<T> _source;
            private readonly IList<T> _buffer;
            private int _index = -1;

            public BufferedEnumerator(IEnumerator<T> source, IList<T> buffer)
            {
                _source = source;
                _buffer = buffer;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _index++;
                if (_index < _buffer.Count)
                {
                    return true;
                }

                if (_source.MoveNext())
                {
                    _buffer.Add(_source.Current);
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                _index = -1;
            }

            public T Current => _buffer[_index];

            object IEnumerator.Current => Current;
        }
    }
}