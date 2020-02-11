using System;
using System.Collections.Generic;
using System.Linq;

public class PriorityQueue<T> where T : IComparable<T>
{
    private readonly List<T> heap;

    public PriorityQueue()
    {
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    public void Enqueue(T item)
    {
        this.Insert(item);
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

    public T Dequeue()
    {
        return this.Pull();
    }

    public void DecreaseKey(T element)
    {
        this.HeapifyUp(this.heap.IndexOf(element));
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && this.IsMore(this.Parent(index), index))
        {
            this.Swap(index, this.Parent(index));
            index = this.Parent(index);
        }
    }

    private bool IsMore(int parent, int child)
    {
        if (this.heap[parent].CompareTo(this.heap[child]) > 0)
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

            if (this.HasChild(child + 1) && this.IsMore(child, child + 1))
            {
                child = child + 1;
            }

            if (this.IsMore(child, index))
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