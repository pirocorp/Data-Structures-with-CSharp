using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinarySearchTree<T> : IBinarySearchTree<T>, IEnumerable<T> where T:IComparable
{
    private class Node
    {
        public T Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node Parent { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }

        public int Count => this.GetCount(this);

        public override string ToString()
        {
            return this.Value.ToString();
        }

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

    public int Count()
    {
        if (this.root == null)
        {
            return 0;
        }

        return this.root.Count;
    }

    public T MinElement()
    {
        this.RootNullCheck();

        return this.MinElement(this.root).Value;
    }

    public T MaxElement()
    {
        this.RootNullCheck();

        return this.MaxElement(this.root).Value;
    }

    public void Insert(T element)
    {
        if (this.root == null)
        {
            this.root = new Node(element);
            return;
        }

        this.Insert(element, this.root);
    }

    public bool Contains(T element)
    {
        var node = this.FindElement(element);

        return node != null;
    }

    public void DeleteMin()
    {
        this.RootNullCheck();


        if (this.root.Left == null)
        {
            this.root = this.root.Right;
            return;
        }

        //this.DeleteMinIterative();
        this.DeleteMinRecursive(this.root);
    }

    public void DeleteMax()
    {
        this.RootNullCheck();

        if (this.root.Right == null)
        {
            this.root = this.root.Left;
            return;
        }

        //this.DeleteMaxIterative();
        this.DeleteMaxRecursive(this.root);
    }

    public void Delete(T element)
    {
        if (this.root == null || !this.Contains(element))
        {
            throw new InvalidOperationException();
        }

        this.root = this.Delete(element, this.root);
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

    public int Rank(T element)
    {
        return this.Rank(element, this.root);
    }

    public T Select(int rank)
    {
        return this.Select(rank, this.root);
    }

    public T Floor(T element)
    {
        var result = this.Floor(element, this.root);

        if (result == null)
        {
            throw new InvalidOperationException();
        }

        return result.Value;
    }

    public T Ceiling(T element)
    {
        var result = this.Ceiling(element, this.root);

        if (result == null)
        {
            throw new InvalidOperationException();
        }
        return result.Value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (this.root == null)
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        return this.Range(this.MinElement(), this.MaxElement(), this.root)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
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

    private Node MinElement(Node node)
    {
        if (node.Left == null)
        {
            return node;
        }

        return this.MinElement(node.Left);
    }

    private Node MaxElement(Node node)
    {
        if (node.Right == null)
        {
            return node;
        }

        return this.MaxElement(node.Right);
    }

    private void Insert(T element, Node node)
    {
        //(element > node.Value) = 1
        if (element.CompareTo(node.Value) > 0)
        {
            //Go Right
            if (node.Right == null)
            {
                node.Right = new Node(element);
                return;
            }


            this.Insert(element, node.Right);
        }

        //(element < node.Value) = -1
        if (element.CompareTo(node.Value) < 0)
        {
            //Go Left
            if (node.Left == null)
            {
                node.Left = new Node(element);
                return;
            }

            this.Insert(element, node.Left);
        }
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

    private void DeleteMaxIterative()
    {
        var node = this.root;
        Node parent = null;

        while (node.Right != null)
        {
            parent = node;
            node = node.Right;
        }

        parent.Right = node.Left;
    }

    private void DeleteMaxRecursive(Node node)
    {
        if (node.Right.Right == null)
        {
            node.Right = node.Right.Left;
            return;
        }

        this.DeleteMinRecursive(node.Left);
    }

    private Node Delete(T element, Node node)
    {
        //element > node.Value = 1
        var compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            node.Left = this.Delete(element, node.Left);
        }
        else if (compare > 0)
        {
            node.Right = this.Delete(element, node.Right);
        }
        else
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }

            node.Value = this.MinElement(node.Right).Value;

            node.Right = this.Delete(node.Value, node.Right);
        }

        return node;
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

        this.EachInOrder(node.Left, action);

        action(node.Value);

        this.EachInOrder(node.Right, action);
    }

    private int Rank(T element, Node node)
    {
        if (node == null)
        {
            return 0;
        }

        var compare = element.CompareTo(node.Value);

        //element < node.Value
        if (compare < 0)
        {
            return this.Rank(element, node.Left);
        }

        if (compare > 0)
        {
            return 1 + (node.Left?.Count ?? 0) + this.Rank(element, node.Right);
        }

        return node.Left?.Count ?? 0;
    }

    private T Select(int rank, Node node)
    {
        if (node == null || this.root.Count < rank)
        {
            throw new InvalidOperationException();
        }

        var compare = rank.CompareTo(this.Rank(node.Value));

        if (compare < 0)
        {
            return this.Select(rank, node.Left);
        }
        if (compare > 0)
        {
            return this.Select(rank, node.Right);
        }

        return node.Value;
    }

    private Node Ceiling(T element, Node node)
    {
        if (node == null)
        {
            return null;
        }

        var comparer = node.Value.CompareTo(element);

        if (comparer <= 0)
        {
            return this.Ceiling(element, node.Right);
        }

        var temp = this.Ceiling(element, node.Left);

        if (temp == null)
        {
            return node;
        }

        return temp;
    }

    private Node Floor(T element, Node node)
    {
        if (node == null)
        {
            return null;
        }

        var comparer = node.Value.CompareTo(element);

        if (comparer >= 0)
        {
            return this.Floor(element, node.Left);
        }

        var temp = this.Floor(element, node.Right);

        if (temp == null)
        {
            return node;
        }
        return temp;
    }

    private void RootNullCheck()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }
    }
}