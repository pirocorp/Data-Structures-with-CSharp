using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private readonly LinkedList<T> _byInsertion;

    private readonly OrderedBag<LinkedListNode<T>> _byOrder;
    private readonly OrderedBag<LinkedListNode<T>> _byOrderReversed;

    public FirstLastList()
    {
        this._byInsertion = new LinkedList<T>();
        this._byOrder = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this._byOrderReversed = new OrderedBag<LinkedListNode<T>>((x, y) => - x.Value.CompareTo(y.Value));
    }

    public int Count => this._byInsertion.Count;

    public void Add(T element)
    {
        var node = new LinkedListNode<T>(element);

        this._byInsertion.AddLast(node);
        this._byOrder.Add(node);
        this._byOrderReversed.Add(node);
    }

    public void Clear()
    {
        this._byInsertion.Clear();
        this._byOrder.Clear();
        this._byOrderReversed.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        ValidateCount(count);

        var current = this._byInsertion.First;

        while (count > 0)
        {
           yield return current.Value;
           current = current.Next;
           count--;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        ValidateCount(count);

        var current = this._byInsertion.Last;

        while (count > 0)
        {
            yield return current.Value;
            current = current.Previous;
            count--;
        }
    }

    private void ValidateCount(int count)
    {
        if (this.Count < count || count < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    public IEnumerable<T> Max(int count)
    {
        ValidateCount(count);

        this.ValidateCount(count);

        foreach (var item in _byOrderReversed)
        {
            if (count <= 0)
            {
                break;
            }

            yield return item.Value;
            count--;
        }
    }

    public IEnumerable<T> Min(int count)
    {
        this.ValidateCount(count);

        foreach (var item in _byOrder)
        {
            if (count <= 0)
            {
                break;
            }

            yield return item.Value;
            count--;
        }
    }

    public int RemoveAll(T element)
    {
        var node = new LinkedListNode<T>(element);
        var range = this._byOrder.Range(node, true, node, true);

        foreach (var item in range)
        {
            _byInsertion.Remove(item);
        }

        var count = this._byOrder.RemoveAllCopies(node);
        this._byOrderReversed.RemoveAllCopies(node);

        return count;
    }
}