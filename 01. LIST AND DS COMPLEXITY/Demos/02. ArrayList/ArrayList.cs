namespace _02._ArrayList
{
    using System;

    public class ArrayList<T>
    {
        private T[] data;

        public int Count { get; private set; }

        public ArrayList(int initialSize = 16)
        {
            this.data = new T[initialSize];
            this.Count = 0;
        }

        public T this[int index]
        {
            get
            {
                this.IndexCheck(index);

                return this.data[index];
            }

            set
            {
                this.IndexCheck(index);

                this.data[index] = value;
            }
        }

        public void Add(T item)
        {
            if (this.Count >= this.data.Length)
            {
                this.Resize(this.Count * 2);
            }

            this.data[this.Count] = item;
            this.Count++;
        }

        public T RemoveAt(int index)
        {
            this.IndexCheck(index);

            T item = this.data[index];

            for (var i = index; i < this.Count - 1; i++)
            {
                this.data[i] = this.data[i + 1];
            }

            this.data[this.Count - 1] = default(T);
            this.Count--;

            if (this.Count <= this.data.Length / 4 && 
                this.data.Length / 4 > 0)
            {
                this.Resize(this.data.Length / 2);
            }

            return item;
        }

        private void Resize(int newSize)
        {
            T[] newArray = new T[newSize];
            Array.Copy(this.data, newArray, this.Count);
            this.data = newArray;
        }

        private void IndexCheck(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}