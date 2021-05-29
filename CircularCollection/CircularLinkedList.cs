using System;
using System.Collections.Generic;
using System.Collections;

namespace CircularCollection
{
    /// <summary>
    /// Rotatable list implemented by linked list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularLinkedList<T> : ICircularCollection<T>
    {
        // 右回りの循環双方向連結リスト
        private Node<T> _head;

        public int Length { get; private set; }

        private Node<T> GetNodeAt(int index)
        {
            if (index < 0 || index >= Length)
                return default;
            var shiftLeft = false;
            if (index >= Length / 2)
            {
                index = Math.Abs(index - Length);
                shiftLeft = true;
            }
            var node = _head;
            for (var i = 0; index > i; i++)
            {
                node = shiftLeft ? node.Left : node.Right;
            }

            return node;
        }

        public T this[int index]
        {
            get
            {
                var node = GetNodeAt(index);
                if (node == default)
                    throw new IndexOutOfRangeException();
                return node.Value;
            }

            set
            {
                var node = GetNodeAt(index);
                if (node == default)
                    throw new IndexOutOfRangeException();
                node.Value = value;
            }
        }
        
        public void Rotate(int shiftRight)
        {
            if (shiftRight < 0)
            {
                var shiftLeft = Math.Abs(shiftRight);
                var m = shiftLeft / Length;
                if (shiftLeft % Length != 0)
                    m++;
                shiftRight += Length * m;
            }

            shiftRight %= Length;
            _head = GetNodeAt(shiftRight);
        }

        private Node<T> AddInCircle(T value)
        {
            var node = new Node<T>(value);
            Length++;
            if (_head == null)
            {
                _head = node;
                _head.Right = node;
                _head.Left = node;
                return node;
            }

            node.Right = _head;
            node.Left = _head.Left;
            node.Left.Right = node;
            _head.Left = node;
            return node;
        }

        public void AddFirst(T value)
        {
            var node = AddInCircle(value);
            _head = node;
        }

        public void AddLast(T value)
        {
            AddInCircle(value);
        }

        private void RemoveFromCircle(Node<T> node)
        {
            Length--;
            if (Length == 1)
            {
                _head = default;
                return;
            }

            if (node == _head)
            {
                _head = node.Right;
            }

            node.Left.Right = node.Right;
            node.Right.Left = node.Left;
        }

        public void Remove(T value)
        {
            var node = _head;
            for (var i = 0; Length > i; i++)
            {
                if (node.Value.Equals(value))
                {
                    RemoveFromCircle(node);
                    return;
                }

                node = node.Right;
            }
        }

        public void RemoveAt(int index)
        {
            var node = GetNodeAt(index);
            if (node == default)
                throw new IndexOutOfRangeException();
            RemoveFromCircle(node);
        }

        private class Node<TE>
        {
            internal TE Value;
            internal Node<TE> Right;
            internal Node<TE> Left;

            internal Node(TE value)
                => Value = value;
        }

        public IEnumerator<T> GetEnumerator()
            => new CircularLinkedListEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private class CircularLinkedListEnumerator<E> : IEnumerator<E>
        {
            private int _nextIndex;
            private readonly CircularLinkedList<E> _list;
            
            public E Current { get; private set; }
            object IEnumerator.Current => Current;

            public CircularLinkedListEnumerator(CircularLinkedList<E> list)
                => _list = list;

            public bool MoveNext()
            {
                if (_nextIndex >= _list.Length)
                    return false;
                Current = _list[_nextIndex];
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