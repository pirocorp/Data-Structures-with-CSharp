namespace _01._Binary_Search_Tree
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> where T : IComparable
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

            public override string ToString()
            {
                return this.Value.ToString();
            }
        }

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

        public BinarySearchTree<T> Search(T element)
        {
            var node = this.FindElement(element);

            return new BinarySearchTree<T>(node);
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                return;
            }

            var node = this.root;
            Node parent = null;

            while (node.Left != null)
            {
                parent = node;
                node = node.Left;
            }

            if (parent == null)
            {
                this.root = this.root.Right;
            }
            else
            {
                parent.Left = node.Right;
            }
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var range = new Queue<T>();

            this.Range(startRange, endRange, range, this.root);

            return range;
        }

        public IEnumerable<T> RangeEnumerator(T startRange, T endRange)
        {
            return this.RangeEnumerator(startRange, endRange, this.root);
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

        private IEnumerable<T> RangeEnumerator(T startRange, T endRange, Node node)
        {
            if (node == null)
            {
                yield break;
            }

            var compareLow = startRange.CompareTo(node.Value);
            var compareHigh = endRange.CompareTo(node.Value);

            if (compareLow < 0)
            {
                foreach (var currentNode in this.RangeEnumerator(startRange, endRange, node.Left))
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
                foreach (var currentNode in this.RangeEnumerator(startRange, endRange, node.Right))
                {
                    yield return currentNode;
                }
            }
        }
    }
}