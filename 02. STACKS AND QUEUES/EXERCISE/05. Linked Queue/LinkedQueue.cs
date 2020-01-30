using System;
using System.Linq.Expressions;

public class LinkedQueue<T>
{
    private QueueNode head;
    private QueueNode tail;

    public int Count { get; private set; }

    public LinkedQueue()
    {
        this.head = null;
        this.tail = null;
        this.Count = 0;
    }

    public void Enqueue(T element)
    {
        var newTail = new QueueNode(element, this.tail, null);

        if (this.Count == 0)
        {
            this.tail = newTail;
            this.head = this.tail;
        }
        else
        {
            this.tail.NextNode = newTail;
            this.tail = newTail;
        }

        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var element = this.head.Value;

        this.head = this.head.NextNode;
        this.Count--;

        return element;
    }

    public T[] ToArray()
    {
        var array = new T[this.Count];

        var current = this.head;
        var index = 0;

        while (current != null)
        {
            array[index++] = current.Value;
            current = current.NextNode;
        }

        return array;
    }

    private class QueueNode
    {
        public T Value { get; private set; }

        public QueueNode NextNode { get; set; }

        public QueueNode PrevNode { get; set; }

        public QueueNode(T value, QueueNode prev, QueueNode next)
        {
            this.Value = value;
            this.PrevNode = prev;
            this.NextNode = next;
        }
    }
}