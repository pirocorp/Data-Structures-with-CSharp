using System.Linq;

    using System;
    using System.Collections.Generic;
    using System.Collections;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node root;
        private readonly Dictionary<T, Node> nodes;

        public Hierarchy(T root)
        {
            this.root = new Node(root);
            this.nodes = new Dictionary<T, Node> {{root, this.root}};
        }

        public void Add(T element, T child)
        {
            if (!this.nodes.ContainsKey(element))
            {
                throw new ArgumentException("Parent does not exists.");
            }

            if (this.nodes.ContainsKey(child))
            {
                throw new ArgumentException("Child already exists");
            }

            var parentNode = this.nodes[element];

            var childNode = new Node(child);

            childNode.Parent = parentNode;
            parentNode.Children.Add(childNode);
            this.nodes.Add(child, childNode);
        }

        public int Count => this.nodes.Count;

        public void Remove(T element)
        {
            if (!this.nodes.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (this.root == this.nodes[element])
            {
                throw new InvalidOperationException();
            }

            var current = this.nodes[element];
            var parent = current.Parent;
            var children = current.Children;

            foreach (var child in children)
            {
                child.Parent = parent;
            }

            parent.Children.Remove(current);
            parent.Children.AddRange(children);
            this.nodes.Remove(element);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.nodes.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            var parent = this.nodes[item];

            return parent.Children.Select(x => x.Value);
        }

        public T GetParent(T item)
        {
            if (!this.nodes.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            var current = this.nodes[item];

            if (current.Parent == null)
            {
                return default(T);
            }

            return current.Parent.Value;
        }

        public bool Contains(T value)
        {
            return this.nodes.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            if (other == null)
            {
                throw new ArgumentException();
            }

            var result = this.Intersect(other);
            return result;
        } 

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node>();

            queue.Enqueue(this.root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }

                yield return current.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node
        {
            public T Value { get; private set; }

            public Node Parent { get; set; }

            public List<Node> Children { get; private set; }

            public Node(T value)
            {
                this.Value = value;
                this.Children = new List<Node>();
            }

            public override string ToString()
            {
                return this.Value.ToString();
            }
        }
    }