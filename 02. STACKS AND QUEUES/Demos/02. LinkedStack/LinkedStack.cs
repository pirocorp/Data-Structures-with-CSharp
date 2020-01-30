namespace _02._LinkedStack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LinkedStack<T> : IEnumerable<T>
    {
        private StackNode top;

        public int Count { get; private set; }

        public LinkedStack()
        {
            this.Count = 0;
            this.top = null;
        }

        public void Push(T element)
        {
            this.top = new StackNode(element, this.top);

            this.Count++;
        }

        public T Pop()
        {
            this.ValidateCount();

            var result = this.top.Value;
            this.top = this.top.Next;
            this.Count--;

            return result;
        }

        public T Peak()
        {
            this.ValidateCount();

            var result = this.top.Value;
            return result;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];

            var currentTop = this.top;
            var currentIndex = 0;

            while (currentTop != null)
            {
                array[currentIndex++] = currentTop.Value;
                currentTop = currentTop.Next;
            }

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.top;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class StackNode
        {
            public T Value { get; private set; }

            public StackNode Next { get; set; }

            public StackNode(T value, StackNode next)
            {
                this.Value = value;
                this.Next = next;
            }
        }

        private void ValidateCount()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}