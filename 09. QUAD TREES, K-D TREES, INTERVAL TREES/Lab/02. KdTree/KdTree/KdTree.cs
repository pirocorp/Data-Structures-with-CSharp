using System;

public class KdTree
{
    private Node _root;

    public KdTree()
    {
    }

    public Node Root => this._root;

    public bool Contains(Point2D point)
    {
        var node = this.FindNode(this.Root, point, 0);

        return node != null;
    }

    private Node FindNode(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return null;
        }

        var cmp = this.Compare(point, node.Point, depth);

        if (cmp < 0)
        {
            return FindNode(node.Left, point, depth + 1);
        }
        else if (cmp > 0)
        {
            return FindNode(node.Right, point, depth + 1);
        }

        return node;
    }

    public void Insert(Point2D point)
    {
        this._root = this.Insert(this._root, point, 0);
    }

    private Node Insert(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return new Node(point);
        }

        var cmp = this.Compare(point, node.Point, depth);

        if (cmp < 0)
        {
            node.Left = Insert(node.Left, point, depth + 1);
        }
        else if (cmp > 0)
        {
            node.Right = Insert(node.Right, point, depth + 1);
        }

        return node;
    }

    private int Compare(Point2D a, Point2D b, int depth)
    {
        var cmp = 0;

        if (depth % 2 == 0)
        {
            cmp = a.X.CompareTo(b.X);

            if (cmp == 0)
            {
                cmp = a.Y.CompareTo(b.Y);
            }

            return cmp;
        }
        else
        {
            cmp = a.Y.CompareTo(b.Y);

            if (cmp == 0)
            {
                cmp = a.X.CompareTo(b.X);
            }
        }

        return cmp;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this._root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }

    public class Node
    {
        public Point2D Point { get; private set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(Point2D point)
        {
            this.Point = point;
        }
    }
}