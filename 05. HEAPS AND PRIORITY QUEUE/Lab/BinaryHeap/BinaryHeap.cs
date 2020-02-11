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
        if (this.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        return this.heap[0];
    }

    public T Pull()
    {
        if (this.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        var item = this.heap[0];

        this.Swap(0, this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);
        this.HeapifyDown(0);

        return item;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && this.IsLess(this.Parent(index), index))
        {
            this.Swap(index, this.Parent(index));
            index = this.Parent(index);
        }
    }

    private bool IsLess(int parent, int child)
    {
        if (this.heap[parent].CompareTo(this.heap[child]) < 0)
        {
            return true;
        }

        return false;
    }

    private void Swap(int x, int y)
    {
        var temp = this.heap[x];
        this.heap[x] = this.heap[y];
        this.heap[y] = temp;
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }

    private void HeapifyDown(int index)
    {
        while (index < this.heap.Count / 2)
        {
            var child = this.Left(index);

            if (this.HasChild(child + 1) && this.IsLess(child, child + 1))
            {
                child = child + 1;
            }

            if (this.IsLess(child, index))
            {
                break;
            }

            this.Swap(index, child);
            index = child;
        }
    }

    private bool HasChild(int index)
    {
        return index < this.Count;
    }

    private int Left(int parent)
    {
        return 2 * parent + 1;
    }
}