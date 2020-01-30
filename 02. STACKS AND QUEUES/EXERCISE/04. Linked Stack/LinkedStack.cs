using System;

public class LinkedStack<T>
{
    private Node firstNode;

    public int Count { get; private set; }

    public LinkedStack()
    {
        this.firstNode = null;
        this.Count = 0;
    }

    public void Push(T element)
    {
        this.firstNode = new Node(element, this.firstNode);
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var element = this.firstNode.Value;
        this.firstNode = this.firstNode.NextNode;

        this.Count--;

        return element;
    }

    public T[] ToArray()
    {
        var array = new T[this.Count];

        var currentElement = this.firstNode;
        var currentIndex = 0;

        while (currentElement != null)
        {
            array[currentIndex++] = currentElement.Value;
            currentElement = currentElement.NextNode;
        }

        return array;
    }

    private class Node
    {
        public T Value { get; private set; }

        public Node NextNode { get; private set; }

        public Node(T value, Node nextNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }
    }
}