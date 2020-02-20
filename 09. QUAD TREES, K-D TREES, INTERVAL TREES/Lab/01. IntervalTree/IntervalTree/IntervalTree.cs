using System;
using System.Collections.Generic;

public class IntervalTree
{
    private Node _root;

    public void Insert(decimal lo, decimal hi)
    {
        this._root = this.Insert(this._root, lo, hi);
    }

    public void EachInOrder(Action<Interval> action)
    {
        EachInOrder(this._root, action);
    }

    public Interval SearchAny(decimal lo, decimal hi)
    {
        var current = this._root;

        while (current != null && !current.Interval.Intersects(lo, hi))
        {
            if (current.Left != null && current.Left.Max > lo)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }

        return current?.Interval;
    }

    public IEnumerable<Interval> SearchAll(decimal lo, decimal hi)
    {
        var result = new List<Interval>();
        this.SearchAll(this._root, lo, hi, result);

        return result;
    }

    private void SearchAll(Node node, decimal lo, decimal hi, List<Interval> result)
    {
        if (node == null)
        {
            return;
        }

        var goLeft = node.Left != null && node.Left.Max > lo;
        var goRight = node.Right != null && node.Right.Interval.Lo < hi;

        if (goLeft)
        {
            this.SearchAll(node.Left, lo, hi, result);
        }

        if (node.Interval.Intersects(lo, hi))
        {
            result.Add(node.Interval);
        }

        if (goRight)
        {
            this.SearchAll(node.Right, lo, hi, result);
        }
    }

    private void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrder(node.Left, action);
        action(node.Interval);
        EachInOrder(node.Right, action);
    }

    private Node Insert(Node node, decimal lo, decimal hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.Interval.Lo);
        if (cmp < 0)
        {
            node.Left = Insert(node.Left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.Right = Insert(node.Right, lo, hi);
        }

        UpdateMax(node);
        return node;
    }

    private static void UpdateMax(Node node)
    {
        var maxChild = GetMax(node.Left, node.Right);
        node.Max = GetMax(node, maxChild).Max;
    }

    private static Node GetMax(Node a, Node b)
    {
        // :D
        return a == null ? b : b == null ? a : a.Max > b.Max ? a : b;
    }

    private class Node
    {
        public Interval Interval { get; }

        public decimal Max { get; set; }

        public Node Right { get; set; }

        public Node Left { get; set; }

        public Node(Interval interval)
        {
            this.Interval = interval;
            this.Max = interval.Hi;
        }
    }
}