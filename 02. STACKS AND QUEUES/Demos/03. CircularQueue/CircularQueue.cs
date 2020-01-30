namespace _03._CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IEnumerable<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] elements;
        private int startIndex;
        private int endIndex;

        public int Count { get; private set; }

        public CircularQueue(int capacity = DEFAULT_CAPACITY)
        {
            this.elements = new T[capacity];
        }

        public void Enqueue(T element)
        {
            if (this.Count >= this.elements.Length)
            {
                this.Resize();
            }

            this.elements[this.endIndex] = element;
            this.endIndex = (this.endIndex + 1) % this.elements.Length;
            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            T result = this.elements[this.startIndex];
            this.elements[this.startIndex] = default(T);
            this.startIndex = (this.startIndex + 1) % this.elements.Length;
            this.Count--;

            return result;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];

            this.CopyAllElements(array);

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentIndex = 0;
            var currentHead = this.startIndex;

            while (currentIndex < this.Count)
            {
                var element = this.elements[currentHead];
                yield return element;
                currentHead = (currentHead + 1) % this.elements.Length;
                currentIndex++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Resize()
        {
            var newArray = new T[this.elements.Length * 2];

            this.CopyAllElements(newArray);

            this.elements = newArray;
            this.startIndex = 0;
            this.endIndex = this.Count;
        }

        private void CopyAllElements(T[] newArray)
        {
            var currentIndex = 0;
            var currentHead = this.startIndex;

            while (currentIndex < this.Count)
            {
                newArray[currentIndex++] = this.elements[currentHead];
                currentHead = (currentHead + 1) % this.elements.Length;
            }
        }
    }
}