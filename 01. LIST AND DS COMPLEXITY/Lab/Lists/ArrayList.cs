using System;

public class ArrayList<T>
{
    private const int INITIAL_CAPACITY = 2;

    private T[] items;

    public int Count { get; private set; }

    public ArrayList()
    {
        this.items = new T[INITIAL_CAPACITY];
        this.Count = 0;
    }

    public T this[int index]
    {
        get
        {
            this.IndexCheck(index);
            return this.items[index];
        }

        set
        {
            this.IndexCheck(index);
            this.items[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count >= this.items.Length)
        {
            this.Resize(this.Count * 2);
        }

        this.items[this.Count] = item;
        this.Count++;
    }

    public T RemoveAt(int index)
    {
        this.IndexCheck(index);

        T item = this.items[index];

        for (var i = index; i < this.Count - 1; i++)
        {
            this.items[i] = this.items[i + 1];
        }

        this.items[this.Count - 1] = default(T);
        this.Count--;

        if (this.Count <= this.items.Length / 4 &&
            this.items.Length / 4 > 0)
        {
            this.Resize(this.items.Length / 2);
        }

        return item;
    }

    private void IndexCheck(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    private void Resize(int newSize)
    {
        T[] newArray = new T[newSize];
        Array.Copy(this.items, newArray, this.Count);
        this.items = newArray;
    }
}
