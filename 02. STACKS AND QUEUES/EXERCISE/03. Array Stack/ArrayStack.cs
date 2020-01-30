using System;
using System.Collections.Generic;
using System.Text;

public class ArrayStack<T>
{
    private const int INITIAL_CAPACITY = 4;

    private T[] elements;

    public int Count { get; private set; }

    public int Capacity => this.elements.Length;

    public ArrayStack(int capacity = INITIAL_CAPACITY)
    {
        this.elements = new T[capacity];
        this.Count = 0;
    }

    public void Push(T element)
    {
        if (this.Count == this.elements.Length)
        {
            this.Grow();
        }

        this.elements[this.Count] = element;
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var element = this.elements[this.Count - 1];
        this.elements[this.Count - 1] = default(T);
        this.Count--;

        return element;
    }

    public T[] ToArray()
    {
        var array = new T[this.Count];
        var destinationIndex = 0;

        for (var sourceIndex = this.Count - 1; sourceIndex >= 0; sourceIndex--)
        {
            array[destinationIndex++] = this.elements[sourceIndex];
        }

        return array;
    }

    private void Grow()
    {
        var newElements = new T[this.elements.Length * 2];
        Array.Copy(this.elements, newElements, this.elements.Length);
        this.elements = newElements;
    }
}