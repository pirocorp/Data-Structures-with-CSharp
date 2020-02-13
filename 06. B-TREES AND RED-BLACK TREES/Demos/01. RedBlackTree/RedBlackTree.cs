namespace _01._RedBlackTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RedBlackTree<TK, TV> where TK : IComparable<TK>
    {
        private Node root;

        private int size;

        public RedBlackTree()
        {
            this.size = 0;
            this.root = null;
        }

        public void Add(TK key, TV value)
        {
            var node = new Node(key, value);

            if (this.root == null)
            {
                this.root = node;
                this.root.IsBlack = true;
                this.size++;

                return;
            }

            this.Add(this.root, node);
            this.size++;
            this.root.IsBlack = true;
        }

        public int Height()
        {
            if (this.root == null)
            {
                return 0;
            }

            return this.Height(this.root) - 1;
        }

        private int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            var leftHeight = this.Height(node?.Left) + 1;
            var rightHeight = this.Height(node?.Right) + 1;

            if (leftHeight > rightHeight)
            {

                return leftHeight;
            }

            return rightHeight;
        }

        public int BlackNodes()
        {
            return this.BlackNodes(this.root);
        }

        private int BlackNodes(Node node)
        {
            if (node == null)
            {
                return 1;
            }

            var rightBlackNodes = this.BlackNodes(node.Right);
            var leftBlackNodes = this.BlackNodes(node.Left);

            if (rightBlackNodes != leftBlackNodes)
            {
                //throw an error
                //or fix a tree
                //throw new Exception("Internal tree exception invalid black nodes count");
            }

            if (node.IsBlack)
            {
                leftBlackNodes++;
            }

            return leftBlackNodes;
        }

        private void Add(Node parent, Node newNode)
        {
            //newNode > parent
            if (newNode.Key.CompareTo(parent.Key) > 0)
            {
                if (parent.Right == null)
                {
                    parent.Right = newNode;
                    newNode.Parent = parent;
                    newNode.IsLeftChild = false;
                    newNode.IsBlack = false;
                }
                else
                {
                    this.Add(parent.Right, newNode);
                }
            }

            //newNode < parent
            if (newNode.Key.CompareTo(parent.Key) < 0)
            {
                if (parent.Left == null)
                {
                    parent.Left = newNode;
                    newNode.Parent = parent;
                    newNode.IsLeftChild = true;
                    newNode.IsBlack = false;
                }
                else
                {
                    this.Add(parent.Left, newNode);
                }
            }

            //Change value if the same key
            if (newNode.Key.CompareTo(parent.Key) == 0)
            {
                parent.Value = newNode.Value;
                return;
            }

            this.CheckColor(newNode);
        }

        private void CheckColor(Node node)
        {
            //root is always black
            //new nodes are red
            //nulls are black
            //no two consecutive red nodes !!!
            //same number of black nodes on every path 

            //if nodes cause for violation
            //black aunt -> rotate
            //red aunt -> color flip

            if (node == this.root)
            {
                return;
            }

            //Handling duplicates
            if (node?.Parent == null)
            {
                return;
            }

            //two consecutive red
            if (!node.IsBlack && !node.Parent.IsBlack)
            {
                this.CorrectTree(node);
            }

            this.CheckColor(node.Parent);
        }

        private void CorrectTree(Node node)
        {
            if (node.Parent.IsLeftChild)
            {
                //aunt = node.Parent.Parent.Right
                //var aunt = node.Parent.Parent?.Right.IsBlack;

                if (node.Parent.Parent.Right == null || node.Parent.Parent.Right.IsBlack)
                {
                    this.root.PrintPretty("", true, "Before Rotation");

                    this.Rotate(node);

                    //this.root.PrintPretty("", true, "After Rotation");
                    return;
                }

                this.root.PrintPretty("", true, "Before Color Flip");


                if (node.Parent.Parent.Right != null)
                {
                    node.Parent.Parent.Right.IsBlack = true;
                }

                node.Parent.Parent.IsBlack = false;
                node.Parent.IsBlack = true;

                //this.root.PrintPretty("", true, "After Color Flip");

                return;
            }

            //aunt = node.Parent.Parent.Left
            //var aunt = node.Parent.Parent?.Left.IsBlack;

            if (!node.Parent.IsLeftChild)
            {
                if (node.Parent.Parent.Left == null || node.Parent.Parent.Left.IsBlack)
                {
                    this.root.PrintPretty("", true, "Before Rotation");

                    this.Rotate(node);

                    //this.root.PrintPretty("", true, "After Rotation");

                    return;
                }

                this.root.PrintPretty("", true, "Before Color Flip");

                if (node.Parent.Parent.Left != null)
                {
                    node.Parent.Parent.Left.IsBlack = true;
                }

                node.Parent.Parent.IsBlack = false;
                node.Parent.IsBlack = true;

                //this.root.PrintPretty("", true, "After Color Flip");

                return;
            }
        }

        //Input node causes the violation
        private void Rotate(Node node)
        {
            if (node.IsLeftChild)
            {
                //if child is left and parent is left -> right rotation
                if (node.Parent.IsLeftChild)
                {
                    this.RightRotate(node.Parent.Parent);
                    node.IsBlack = false;
                    node.Parent.IsBlack = true;

                    if (node.Parent.Right != null)
                    {
                        node.Parent.Right.IsBlack = false;
                    }

                    return;
                }

                //if parent right child is left -> right<->left rotation
                this.RightLeftRotate(node.Parent.Parent);
                node.IsBlack = true;
                node.Right.IsBlack = false;
                node.Left.IsBlack = false;
                return;
            }

            if (!node.IsLeftChild)
            {
                //if child is right and parent is right -> left rotation
                if (!node.Parent.IsLeftChild)
                {
                    this.LeftRotate(node.Parent.Parent);
                    node.IsBlack = false;
                    node.Parent.IsBlack = true;

                    if (node.Parent.Left != null)
                    {
                        node.Parent.Left.IsBlack = false;
                    }

                    return;
                }

                //if parent left child is right -> left<->right rotation
                this.LeftRightRotate(node.Parent.Parent);
                node.IsBlack = true;
                node.Right.IsBlack = false;
                node.Left.IsBlack = false;
                return;
            }
        }

        private void LeftRotate(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;

            if (node.Right != null)
            {
                node.Right.Parent = node;
                node.Right.IsLeftChild = false;
            }

            if (node.Parent == null)
            {
                this.root = temp;
                temp.Parent = null;
            }
            else
            {
                temp.Parent = node.Parent;

                if (node.IsLeftChild)
                {
                    temp.IsLeftChild = true;
                    temp.Parent.Left = temp;
                }
                else
                {
                    temp.IsLeftChild = false;
                    temp.Parent.Right = temp;
                }
            }

            temp.Left = node;
            node.IsLeftChild = true;
            node.Parent = temp;
        }

        private void RightRotate(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;

            if (node.Left != null)
            {
                node.Left.Parent = node;
                node.Left.IsLeftChild = false;
            }

            if (node.Parent == null)
            {
                this.root = temp;
                temp.Parent = null;
            }
            else
            {
                temp.Parent = node.Parent;

                if (!node.IsLeftChild)
                {
                    temp.IsLeftChild = false;
                    temp.Parent.Right = temp;
                }
                else
                {
                    temp.IsLeftChild = true;
                    temp.Parent.Left = temp;
                }
            }

            temp.Right = node;
            node.IsLeftChild = false;
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
            public TK Key { get; private set; }

            public TV Value { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public Node Parent { get; set; }

            public bool IsLeftChild { get; set; }

            public bool IsBlack { get; set; }

            public Node(TK key, TV value)
            {
                this.Key = key;
                this.Value = value;
                this.Left = this.Right = this.Parent = null;
                this.IsBlack = false;
                this.IsLeftChild = false;
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
                    if (this.IsLeftChild)
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

                if (this.IsBlack)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine(this.Value.ToString());

                Console.ForegroundColor = ConsoleColor.Gray;

                this.Left?.PrintPretty(indent, this.Right == null, null);
                this.Right?.PrintPretty(indent, true, null);
            }
        }
    }
}