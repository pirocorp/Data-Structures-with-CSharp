using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root => this.root;

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        node = Balance(node);
        UpdateHeight(node);

        return node;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private static int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private static void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private static Node<T> Balance(Node<T> node)
    {
        var balance = Height(node.Left) - Height(node.Right);

        if (balance < -1) //right child is heavy
        {
            //rotate node left
            balance = Height(node.Right.Left) - Height(node.Right.Right);

            if (balance <= 0) //Right subtree Right Child is causing disbalance
            {
                return RotateLeft(node);
            }
            else //Right subtree Left Child is causing disbalance
            {
                //Right - Left Rotation
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
        }
        else if(balance > 1) //Left child is heavy
        {
            //rotate node right
            balance = Height(node.Left.Right) - Height(node.Left.Left);

            if (balance <= 0) //Left subtree Left Child is causing disbalance
            {
                return RotateRight(node);
            }
            else //Left subtree Right Child is causing disbalance
            {
                //Left - Right rotation
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
        }

        return node;
    }

    private static Node<T> RotateLeft(Node<T> node)
    {
        var temp = node.Right;
        node.Right = node.Right.Left;
        temp.Left = node;

        UpdateHeight(node);

        return temp;
    }

    private static Node<T> RotateRight(Node<T> node)
    {
        var temp = node.Left;
        node.Left = node.Left.Right;
        temp.Right = node;

        UpdateHeight(node);

        return temp;
    }
}
