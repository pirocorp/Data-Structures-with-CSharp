namespace _01._Priority_Queue
{
    using System;

    public class AnotherBinaryHeap<T> where T : IComparable<T>
    {
        //Children -> 2 x parent + 1; 2 x parent + 2
        //Parent -> Floor((child - 1) / 2)
        private const int INITIAL_CAPACITY = 16;

        private int lastElementIndex;
        private T[] array;

        public int Count => this.lastElementIndex + 1;

        public AnotherBinaryHeap()
        {
            this.lastElementIndex = -1;
            this.array = new T[INITIAL_CAPACITY];
        }

        public void Add(T element)
        {
            if (this.array.Length <= this.lastElementIndex + 1)
            {
                this.Resize();
            }

            this.array[++this.lastElementIndex] = element;
            this.TrickleUp(this.lastElementIndex);
        }

        public T Peak()
        {
            return this.array[0]; 
        }

        public T Remove()
        {
            var result = this.array[0];

            this.Swap(0, this.lastElementIndex);

            //this.array[this.lastElementIndex] = default(T);
            this.lastElementIndex--;

            this.TrickleDown(0);

            return result;
        }

        private void TrickleDown(int parent)
        {
            var left = 2 * parent + 1;
            var right = 2 * parent + 2;

            if (left == this.lastElementIndex &&
                this.array[parent].CompareTo(this.array[left]) < 0)
            {
                this.Swap(parent, left);
                return;
            }

            if (right == this.lastElementIndex &&
                this.array[parent].CompareTo(this.array[right]) < 0)
            {
                this.Swap(parent, right);
                return;
            }

            if (left >= this.lastElementIndex || right >= this.lastElementIndex)
            {
                return;
            }

            if (this.array[left].CompareTo(this.array[right]) > 0 &&
                this.array[parent].CompareTo(this.array[left]) < 0)
            {
                this.Swap(parent, left);
                this.TrickleDown(left);
            }
            else if(this.array[parent].CompareTo(this.array[right]) < 0)
            {
                this.Swap(parent, right);
                this.TrickleDown(right);
            }
        }

        private void Resize()
        {
            var newArr = new T[this.array.Length * 2];
            Array.Copy(this.array, newArr, this.array.Length);

            this.array = newArr;
        }

        private void TrickleUp(int position)
        {
            if (position == 0)
            {
                return;
            }

            var parent = (position - 1) / 2;

            if (this.array[position].CompareTo(this.array[parent]) > 0)
            {
                this.Swap(position, parent);
                this.TrickleUp(parent);
            }
        }

        private void Swap(int from, int to)
        {
            var temp = this.array[from];
            this.array[from] = this.array[to];
            this.array[to] = temp;
        }
    }
}