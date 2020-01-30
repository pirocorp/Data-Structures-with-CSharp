using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public T Value { get; private set; }

        public Node Next { get; set; }

        public Node(T value, Node next)
        {
            this.Value = value;
            this.Next = next;
        }
    }

    public Node Head { get; private set; }

    public Node Tail { get; private set; }

    public int Count { get; private set; }

    public LinkedList()
    {
        this.Head = this.Tail = null;
        this.Count = 0;
    }

    public void AddFirst(T item)
    {
        this.Head = new Node(item, this.Head);

        if (this.IsEmpty())
        {
            Tail = Head;
        }

        this.Count++;
    }

    public void AddLast(T item)
    {
        var old = this.Tail;
        this.Tail = new Node(item, default(Node));

        if (IsEmpty())
        {
            this.Head = this.Tail;
        }
        else
        {
            old.Next = this.Tail;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.IsEmpty())
        {
            throw new InvalidOperationException();
        }

        var item = this.Head.Value;
        this.Head = this.Head.Next;
        this.Count--;

        if (this.IsEmpty())
        {
            this.Tail = null;
        }

        return item;
    }

    public T RemoveLast()
    {
        if (this.IsEmpty())
        {
            throw new InvalidOperationException();
        }

        var item = this.Tail.Value;

        if (this.Count == 1)
        {
            this.Head = this.Tail = null;
        }
        else
        {
            var newTail = this.GetSecondToLast();
            newTail.Next = null;
            this.Tail = newTail;
        }

        this.Count--;
        return item;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.Head;

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

    private bool IsEmpty()
    {
        return this.Count == 0;
    }

    private Node GetSecondToLast()
    {
        var current = this.Head;

        while (current != null)
        {
            if (current.Next == this.Tail)
            {
                return current;
            }

            current = current.Next;
        }

        return null;
    }
}