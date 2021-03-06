﻿using System;
using System.Collections.Generic;

public class RedBlackTree<T> : IBinarySearchTree<T> where T : IComparable
{
    private const bool RED = true;
    private const bool BLACK = false;

    private Node root;

    public RedBlackTree()
    {
    }

    private RedBlackTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    public int Count()
    {
        return this.Count(this.root);
    }

    private int Count(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Count;
    }

    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }

    public IBinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new RedBlackTree<T>(current);
    }

    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
        this.root.Color = BLACK;
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element, RED);
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        if (this.IsRed(node.Right) && !this.IsRed(node.Left))
        {
            node = this.LeftRotation(node);
        }

        if (this.IsRed(node.Left) && this.IsRed(node.Left.Left))
        {
            node = this.RightRotation(node);
        }

        if (this.IsRed(node.Left) && this.IsRed(node.Right))
        {
            this.FlipColors(node);
        }

        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);
        return node;
    }

    private bool IsRed(Node node)
    {
        if (node == null)
        {
            return false;
        }

        return node.Color == RED;
    }

    private Node LeftRotation(Node node)
    {
        var temp = node.Right;
        node.Right = temp.Left;
        temp.Left = node;

        temp.Color = node.Color;
        node.Color = RED;
        temp.Count = node.Count;
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return temp;
    }

    private Node RightRotation(Node node)
    {
        var temp = node.Left;
        node.Left = temp.Right;
        temp.Right = node;

        temp.Color = node.Color;
        node.Color = RED;
        temp.Count = node.Count;
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return temp;
    }

    private void FlipColors(Node node)
    {
        node.Color = RED;
        node.Left.Color = BLACK;
        node.Right.Color = BLACK;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
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

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        this.root = this.DeleteMin(this.root);
    }

    private Node DeleteMin(Node node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = this.DeleteMin(node.Left);
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return node;
    }

    public virtual void Delete(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        this.root = this.Delete(element, this.root);
    }

    private Node Delete(T element, Node node)
    {
        if (node == null)
        {
            return null;
        }

        int compare = element.CompareTo(node.Value);

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
            if (node.Right == null)
            {
                return node.Left;
            }
            if (node.Left == null)
            {
                return node.Right;
            }

            Node temp = node;
            node = this.FindMin(temp.Right);
            node.Right = this.DeleteMin(temp.Right);
            node.Left = temp.Left;

        }
        node.Count = this.Count(node.Left) + this.Count(node.Right) + 1;

        return node;
    }

    private Node FindMin(Node node)
    {
        if (node.Left == null)
        {
            return node;
        }

        return this.FindMin(node.Left);
    }

    public int Rank(T element)
    {
        return this.Rank(element, this.root);
    }

    private int Rank(T element, Node node)
    {
        if (node == null)
        {
            return 0;
        }

        int compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            return this.Rank(element, node.Left);
        }

        if (compare > 0)
        {
            return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
        }

        return this.Count(node.Left);
    }

    public T Select(int rank)
    {
        Node node = this.Select(rank, this.root);
        if (node == null)
        {
            throw new InvalidOperationException();
        }

        return node.Value;
    }

    private Node Select(int rank, Node node)
    {
        if (node == null)
        {
            return null;
        }

        int leftCount = this.Count(node.Left);
        if (leftCount == rank)
        {
            return node;
        }

        if (leftCount > rank)
        {
            return this.Select(rank, node.Left);
        }
        else
        {
            return this.Select(rank - (leftCount + 1), node.Right);
        }
    }

    public T Ceiling(T element)
    {
        return this.Select(this.Rank(element) + 1);
    }

    public T Floor(T element)
    {
        return this.Select(this.Rank(element) - 1);
    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        this.root = this.DeleteMax(this.root);
    }

    private Node DeleteMax(Node node)
    {
        if (node.Right == null)
        {
            return node.Left;
        }

        node.Right = this.DeleteMax(node.Right);
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return node;
    }

    private class Node
    {
        public T Value { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public int Count { get; set; }

        public bool Color { get; set; }

        public Node(T value, bool color)
        {
            this.Value = value;
            this.Color = color;
        }
    }
}