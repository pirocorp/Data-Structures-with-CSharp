using System;
using System.Collections;
using System.Collections.Generic;

public class ReversedList<T> : IEnumerable<T>
{
    private const int INITIAL_CAPACITY = 2;

    private T[] items;

    public ReversedList()
    {
        this.items = new T[INITIAL_CAPACITY];
        this.Count = 0;
    }

    public int Count { get; private set; }

    public int Capacity => this.items.Length;

    public T this[int index]
    {
        get
        {
            this.IndexCheck(index);
            
            return this.items[this.ReversedIndex(index)];
        }
        set
        {
            this.IndexCheck(index);
            this.items[this.ReversedIndex(index)] = value;
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
        var realIndex = this.ReversedIndex(index);

        T item = this.items[realIndex];

        for (var i = realIndex; i < this.Count - 1; i++)
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

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = this.Count - 1; i >= 0; i--)
        {
            yield return this.items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
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

    private int ReversedIndex(int index)
    {
        var lastElementIndex = this.Count - 1;
        return lastElementIndex - index;
    }
}