﻿using System;
using System.Collections.Generic;

public class Tree<T>
{
    public T Value { get; set; }

    public IList<Tree<T>> Children { get; private set; }

    public Tree<T> Parent { get; set; }

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = new List<Tree<T>>(children);
    }

    public void Print(int indent = 0)
    {
        Console.Write(new string(' ', indent * 2));
        Console.WriteLine(this.Value);

        foreach (var child in this.Children)
        {
            child.Print(indent + 1);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);

        foreach (var child in this.Children)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        var result = new List<T>();

        this.DFS(this, result);

        return result;
    }

    public IEnumerable<T> OrderBFS()
    {
        var result = new List<T>();
        var queue = new Queue<Tree<T>>();

        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();
            result.Add(currentNode.Value);

            foreach (var child in currentNode.Children)
            {
                queue.Enqueue(child);
            }
        }

        return result;
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }

    private void DFS(Tree<T> tree, List<T> result)
    {
        foreach (var child in tree.Children)
        {
            DFS(child, result);
        }

        result.Add(tree.Value);
    }
}