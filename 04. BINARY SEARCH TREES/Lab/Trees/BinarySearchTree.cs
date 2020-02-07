using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private class Node
    {
        public T Value { get; private set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node Parent { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }

        public int Count => this.GetCount(this);

        private int GetCount(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + this.GetCount(node.Left) + this.GetCount(node.Right);
        }
    };

    private Node root;

    public BinarySearchTree()
    {
        
    }

    private BinarySearchTree(Node node)
    {
        this.Copy(node);
    }

    public void Insert(T element)
    {
        if (this.root == null)
        {
            this.root = new Node(element);
            return;
        }

        Node parent = null;
        var node = this.root;

        while (node != null)
        {
            parent = node;

            if (node.Value.CompareTo(element) > 0)
            {
                node = node.Left;
            }
            else if (node.Value.CompareTo(element) < 0)
            {
                node = node.Right;
            }
            else
            {
                return;
            }
        }

        var current = new Node(element);

        if (parent.Value.CompareTo(element) > 0)
        {
            parent.Left = current;
            current.Parent = parent;
        }
        else
        {
            parent.Right = current;
            current.Parent = parent;
        }
    }

    public bool Contains(T element)
    {
        var node = this.FindElement(element);

        return node != null;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        if (this.root.Left == null)
        {
            this.root = this.root.Right;
            return;
        }

        //this.DeleteMinIterative();
        this.DeleteMinRecursive(this.root);
    }

    public BinarySearchTree<T> Search(T item)
    {
        var node = this.FindElement(item);

        return new BinarySearchTree<T>(node);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        //Recursive Generator 
        return this.Range(startRange, endRange, this.root);

        //Using Another Data Structure
        //return this.Range(this.root, startRange, endRange);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }
	
	    public void Delete(T element)
    {
        throw new NotImplementedException();
    }

    public void DeleteMax()
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public int Rank(T element)
    {
        throw new NotImplementedException();
    }

    public T Select(int rank)
    {
        throw new NotImplementedException();
    }

    public T Ceiling(T element)
    {
        throw new NotImplementedException();
    }

    public T Floor(T element)
    {
        throw new NotImplementedException();
    }

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    private Node FindElement(T element)
    {
        var node = this.root;

        while (node != null)
        {
            if (node.Value.CompareTo(element) > 0)
            {
                node = node.Left;
            }
            else if (node.Value.CompareTo(element) < 0)
            {
                node = node.Right;
            }
            else
            {
                break;
            }
        }

        return node;
    }

    private void DeleteMinIterative()
    {
        var node = this.root;
        Node parent = null;

        while (node.Left != null)
        {
            parent = node;
            node = node.Left;
        }

        parent.Left = node.Right;
    }

    private void DeleteMinRecursive(Node node)
    {
        if (node.Left.Left == null)
        {
            node.Left = node.Left.Right;
            return;
        }

        this.DeleteMinRecursive(node.Left);
    }

    private IEnumerable<T> Range(Node node, T startRange, T endRange)
    {
        var range = new Queue<T>();

        this.Range(startRange, endRange, range, node);

        return range;
    }

    private void Range(T startRange, T endRange, Queue<T> range, Node node)
    {
        if (node == null)
        {
            return;
        }

        var compareLow = startRange.CompareTo(node.Value);
        var compareHigh = endRange.CompareTo(node.Value);

        if (compareLow < 0)
        {
            this.Range(startRange, endRange, range, node.Left);
        }

        if (compareLow <= 0 && compareHigh >= 0)
        {
            range.Enqueue(node.Value);
        }

        if (compareHigh > 0)
        {
            this.Range(startRange, endRange, range, node.Right);
        }
    }

    private IEnumerable<T> Range(T startRange, T endRange, Node node)
    {
        if (node == null)
        {
            yield break;
        }

        var compareLow = startRange.CompareTo(node.Value);
        var compareHigh = endRange.CompareTo(node.Value);

        if (compareLow < 0)
        {
            foreach (var currentNode in this.Range(startRange, endRange, node.Left))
            {
                yield return currentNode;
            }
        }

        if (compareLow <= 0 && compareHigh >= 0)
        {
            yield return node.Value;
        }

        if (compareHigh > 0)
        {
            foreach (var currentNode in this.Range(startRange, endRange, node.Right))
            {
                yield return currentNode;
            }
        }
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        if (node.Left != null)
        {
            this.EachInOrder(node.Left, action);
        }

        action(node.Value);

        if (node.Right != null)
        {
            this.EachInOrder(node.Right, action);
        }
    }
}