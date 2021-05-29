using System;
using System.Collections;
using System.Collections.Generic;

namespace CircularCollection
{
    /// <summary>
    /// Rotatable array
    /// </summary>
    public class CircularArray<T> : ICircularCollection<T>
    {
        private readonly T[] _items;
        private int _head;

        public int Length => _items.Length;
        
        public CircularArray(int size)
            => _items = new T[size];
        
        private int Convert(int index)
        {
            if (index < 0)
                index += _items.Length;
            return (_head + index) % _items.Length;
        }
        
        /// <summary>
        /// Rotate array
        /// </summary>
        public void Rotate(int shiftRight)
            => _head = Convert(shiftRight);

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Length)
                    throw new IndexOutOfRangeException();
                return _items[Convert(index)];
            }
            set
            {
                if (index < 0 || index >= _items.Length)
                    throw new IndexOutOfRangeException();
                _items[Convert(index)] = value;
            }
        }

        public IEnumerator<T> GetEnumerator() 
            => new CircularArrayEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private class CircularArrayEnumerator<E> : IEnumerator<E>
        {
            private int _nextIndex;
            private readonly CircularArray<E> _array;
            public E Current { get; private set; }

            object IEnumerator.Current => Current;

            public CircularArrayEnumerator(CircularArray<E> array)
                => this._array = array;

            public bool MoveNext()
            {
                if (_nextIndex >= _array.Length)
                    return false;
                Current = _array[_nextIndex];
                _nextIndex++;
                return true;
            }

            public void Dispose() { }

            public void Reset()
            {
                _nextIndex = 0;
                Current = default;
            }
        }
    }
}