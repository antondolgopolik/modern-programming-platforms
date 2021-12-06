using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab9
{
    public class DynamicList<T> : IEnumerable<T>
    {
        public T[] Items { get; private set; } = new T[20];
        public int Count { get; private set; }

        public void Add(T item)
        {
            if (Count >= Items.Length)
            {
                Expand();
            }

            Items[Count] = item;
            Count++;
        }

        private void Expand()
        {
            var newItems = new T[Items.Length * 2];
            Items.CopyTo(newItems, 0);
            Items = newItems;
        }

        public void Remove()
        {
            if (Count == 0)
            {
                throw new Exception("The list is empty");
            }

            Count--;
        }

        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= Count))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            for (int i = index + 1; i < Count; i++)
            {
                Items[i - 1] = Items[i];
            }

            Count--;
        }

        public void Clear()
        {
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DynamicListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class DynamicListEnumerator : IEnumerator<T>
        {
            private readonly DynamicList<T> _dynamicList;
            private int _index = -1;

            public DynamicListEnumerator(DynamicList<T> dynamicList)
            {
                _dynamicList = dynamicList;
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _dynamicList.Count;
            }

            public void Reset()
            {
                _index = -1;
            }

            public T Current => _dynamicList.Items[_index];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}