namespace _01._AVL_Tree
{
    using System;

    public class AvlTree<T> where T : IComparable<T>
    {
        private Node root;
        private int currentSize;

        public int Count => this.currentSize;

        public AvlTree()
        {
            this.root = null;
            this.currentSize = 0;
        }

        public void Add(T element)
        {
            var node = new Node(element);

            if (this.root == null)
            {
                this.root = node;
                this.currentSize++;
                return;
            }

            this.Add(this.root, node);
        }

        private void Add(Node parent, Node newNode)
        {
            //newNode > parent
            if (newNode.Data.CompareTo(parent.Data) > 0)
            {
                if (parent.Right == null)
                {
                    parent.Right = newNode;
                    newNode.Parent = parent;
                    this.currentSize++;
                }
                else
                {
                    this.Add(parent.Right, newNode);
                }
            }
            else if(newNode.Data.CompareTo(parent.Data) < 0)
            {
                if (parent.Left == null)
                {
                    parent.Left = newNode;
                    newNode.Parent = parent;
                    this.currentSize++;
                }
                else
                {
                    this.Add(parent.Left, newNode);
                }
            }

            this.CheckBalance(newNode);
        }

        private void CheckBalance(Node node)
        {
            if (Math.Abs(this.Height(node.Left) - this.Height(node.Right)) > 1)
            {
                this.Rebalance(node);
            }

            if (node.Parent == null)
            {
                return;
            }

            this.CheckBalance(node.Parent);
        }

        private void Rebalance(Node node)
        {
            if (this.Height(node.Left) - this.Height(node.Right) > 1)
            {
                if (this.Height(node.Left.Left) > this.Height(node.Left.Right))
                {
                    this.RightRotate(node);
                }
                else
                {
                    this.LeftRightRotate(node);
                }
            }
            else
            {
                if (this.Height(node.Right.Right) > this.Height(node.Right.Left))
                {
                    this.LeftRotate(node);
                }
                else
                {
                    this.RightLeftRotate(node);
                }
            }

            if (node.Parent == null)
            {
                this.root = node;
            }
        }

        private int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));
        }

        private void LeftRotate(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;

            if (node.Right != null)
            {
                node.Right.Parent = node;
            }

            if (node.Parent == null)
            {
                this.root = temp;
                temp.Parent = null;
            }
            else
            {
                temp.Parent = node.Parent;

                if (node.IsLeftChild())
                {
                    temp.Parent.Left = temp;
                }
                else
                {
                    temp.Parent.Right = temp;
                }
            }

            temp.Left = node;
            node.Parent = temp;
        }

        private void RightRotate(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;

            if (node.Left != null)
            {
                node.Left.Parent = node;
            }

            if (node.Parent == null)
            {
                this.root = temp;
                temp.Parent = null;
            }
            else
            {
                temp.Parent = node.Parent;

                if (!node.IsLeftChild())
                {
                    temp.Parent.Right = temp;
                }
                else
                {
                    temp.Parent.Left = temp;
                }
            }

            temp.Right = node;
            node.Parent = temp;
        }

        private void RightLeftRotate(Node node)
        {
            this.RightRotate(node.Right);
            this.LeftRotate(node);
        }

        private void LeftRightRotate(Node node)
        {
            this.LeftRotate(node.Left);
            this.RightRotate(node);
        }

        public void Print(string message = null)
        {
            this.root.PrintPretty("", true, message);
            Console.WriteLine(new string('-', 200));
        }

        private class Node
        {
            public T Data { get; private set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public Node Parent { get; set; }

            public bool IsLeftChild()
            {
                if (this.Parent == null)
                {
                    return false;
                }

                return this.Parent.Left == this;
            }

            public Node(T data)
            {
                this.Data = data;
                this.Parent = this.Left = this.Right = null;
            }

            public void PrintPretty(string indent, bool last, string message)
            {
                if (message != null)
                {
                    Console.WriteLine(message);
                }

                Console.Write(indent);
                if (last)
                {
                    if (this.IsLeftChild())
                    {
                        Console.Write("/-");
                    }
                    else
                    {
                        Console.Write("\\-");
                    }

                    indent += "  ";
                }
                else
                {
                    Console.Write("|-");
                    indent += "| ";
                }

                Console.WriteLine(this.Data.ToString());

                this.Left?.PrintPretty(indent, this.Right == null, null);
                this.Right?.PrintPretty(indent, true, null);
            }

            public override string ToString()
            {
                return this.Data.ToString();
            }
        }
    }
}