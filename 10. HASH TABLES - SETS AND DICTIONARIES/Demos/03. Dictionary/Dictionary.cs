namespace _03._Dictionary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable<TKey>
    {
        private const int DEFAULT_CAPACITY = 16;
        private const float FILL_FACTOR = 0.75F;

        private int _numElements;
        private LinkedList<Node>[] _hashArray;

        public Dictionary(int initialSize = DEFAULT_CAPACITY)
        {
            this._hashArray = new LinkedList<Node>[initialSize];
            this.InitializeHashArray();
        }

        public int Count => this._numElements;

        public TValue this[TKey key]
        {
            get => this.GetValue(key);
            set => this.SetValue(key, value);
        }
        
        private double LoadFactor => this._numElements / (double) this._hashArray.Length;

        public void Add(TKey key, TValue value)
        {
            if (this.LoadFactor > FILL_FACTOR)
            {
                this.Resize(this._hashArray.Length * 2);
            }

            var node = this.GetElement(key);

            if (node != null)
            {
                throw new ArgumentException("Key already exists");
            }

            node = new Node(key, value);

            var hash = this.GetHash(key);

            this._hashArray[hash].AddFirst(node);
            this._numElements++;
        }

        public void Remove(TKey key)
        {
            var hash = this.GetHash(key);
            var element = this.GetElement(key);

            if (element == null)
            {
                return;
            }

            this._hashArray[hash].Remove(element);
            this._numElements--;
        }

        public TValue GetValue(TKey key)
        {
            var element = this.GetElement(key);
            return element.Value;
        }

        public void SetValue(TKey key, TValue value)
        {
            var element = this.GetElement(key);

            if (element == null)
            {
                this.Add(key, value);
                this._numElements++;
                return;
            }

            element.Value = value;
        }

        public bool Contains(TKey key)
        {
            var element = this.GetElement(key);

            return element != null;
        }

        public IEnumerable<TValue> Values()
        {
            var nodes = this.EnumerateNodes();

            foreach (var node in nodes)
            {
                yield return node.Value;
            }
        }

        public IEnumerable<TKey> Keys()
        {
            var nodes = this.EnumerateNodes();

            foreach (var node in nodes)
            {
                yield return node.Key;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var nodes = this.EnumerateNodes();

            foreach (var node in nodes)
            {
                yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int GetHash(TKey key, int size = -1)
        {
            if (size == -1)
            {
                size = this._hashArray.Length;
            }

            var hash = key.GetHashCode();
            hash &= 0x7FFFFFFF;
            hash %= size;

            return hash;
        }

        private void Resize(int newSize)
        {
            var newHashArray = new LinkedList<Node>[newSize];

            for (var i = 0; i < newSize; i++)
            {
                newHashArray[i] = new LinkedList<Node>();
            }

            foreach (var (key, value) in this)
            {
                var node = new Node(key, value);

                var hash = this.GetHash(node.Key, newSize);
                newHashArray[hash].AddFirst(node);
            }

            this._hashArray = newHashArray;
        }

        private Node GetElement(TKey key)
        {
            var hash = this.GetHash(key);
            var element = this._hashArray[hash]
                .FirstOrDefault(x => x.Key.CompareTo(key) == 0);

            return element;
        }

        private class Node : IComparable<Node> 
        {
            public TKey Key { get; private set; }

            public TValue Value { get; set; }

            public Node(TKey key, TValue value)
            {
                this.Key = key;
                this.Value = value;
            }

            public int CompareTo(Node other)
            {
                return this.Key.CompareTo(other.Key);
            }

            public override string ToString()
            {
                return $"{this.Key} - {this.Value}";
            }
        }

        private void InitializeHashArray()
        {
            for (var i = 0; i < this._hashArray.Length; i++)
            {
                this._hashArray[i] = new LinkedList<Node>();
            }
        }

        private IEnumerable<Node> EnumerateNodes()
        {
            for (var i = 0; i < this._hashArray.Length; i++)
            {
                var current = this._hashArray[i];

                foreach (var node in current)
                {
                    yield return node;
                }
            }
        }
    }
}