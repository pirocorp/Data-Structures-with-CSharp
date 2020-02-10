namespace _01._Priority_Queue
{
    using System;
    using System.Collections.Generic;

    public class BinaryHeap<T> where T : IComparable<T>
    {
        private readonly List<T> heap;

        public BinaryHeap()
        {
            this.heap = new List<T>();
        }

        public int Count => this.heap.Count;

        public void Insert(T item)
        {
            this.heap.Add(item);
            this.HeapifyUp(this.heap.Count - 1);
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.heap[0];
        }

        public T Pull()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var firstElementIndex = 0;
            var lastElementIndex = this.heap.Count - 1;

            var result = this.heap[firstElementIndex];

            this.heap[firstElementIndex] = this.heap[lastElementIndex];
            this.heap.RemoveAt(lastElementIndex);

            this.HeapifyDown(firstElementIndex);

            return result;
        }

        private void HeapifyUp(int index)
        {
            var element = this.heap[index];
            var parent = this.heap[(index - 1) / 2];

            //element > parent
            while (element.CompareTo(parent) > 0)
            {
                this.heap[index] = parent;
                this.heap[(index - 1) / 2] = element;

                index = (index - 1) / 2;

                element = this.heap[index];
                parent = this.heap[(index - 1) / 2];
            }
        }

        private void HeapifyDown(int index)
        {
            while (index < this.Count / 2)
            {
                var element = this.heap[index];
                var childIndex = 2 * index + 1;

                //this.heap[childIndex] < this.heap[childIndex + 1]
                if (childIndex + 1 < this.Count && 
                    this.heap[childIndex].CompareTo(this.heap[childIndex + 1]) < 0)
                {
                    childIndex++;
                }

                var child = this.heap[childIndex];

                //child > element
                if (child.CompareTo(element) < 0)
                {
                    break;
                }

                this.heap[index] = child;
                this.heap[childIndex] = element;

                index = childIndex;
            }
        }
    }
}