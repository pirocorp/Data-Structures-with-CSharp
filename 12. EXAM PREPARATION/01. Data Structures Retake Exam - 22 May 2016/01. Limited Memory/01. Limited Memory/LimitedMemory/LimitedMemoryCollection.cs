namespace LimitedMemory
{
    using System.Collections.Generic;
    using System.Collections;

    public class LimitedMemoryCollection<TKey, TValue> : ILimitedMemoryCollection<TKey, TValue>
    {
        private readonly Dictionary<TKey, LinkedListNode<Pair<TKey, TValue>>> _elements;
        private readonly LinkedList<Pair<TKey, TValue>> _priority;
        private readonly int _capacity;

        public LimitedMemoryCollection(int capacity)
        {
            this._elements = new Dictionary<TKey, LinkedListNode<Pair<TKey, TValue>>>();
            this._priority = new LinkedList<Pair<TKey, TValue>>();
            this._capacity = capacity;
        }

        public int Capacity => this._capacity;

        public int Count => this._elements.Count;

        public void Set(TKey key, TValue value)
        {
            if (!this._elements.ContainsKey(key))
            {
                if (this.Count == this.Capacity)
                {
                    this.RemoveOldestElement();
                }

                this.AddElement(key, value);
            }
            else
            {
                var node = this._elements[key];
                node.Value.Value = value;
                this._priority.Remove(node);
                this._priority.AddFirst(node);
            }
        }

        public TValue Get(TKey key)
        {
            if (!this._elements.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            var node = this._elements[key];
            this._priority.Remove(node);
            this._priority.AddFirst(node);

            return node.Value.Value;
        }

        public IEnumerator<Pair<TKey, TValue>> GetEnumerator()
        {
            return this._priority.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void AddElement(TKey key, TValue value)
        {
            var pair = new Pair<TKey, TValue>(key, value);
            var node = new LinkedListNode<Pair<TKey, TValue>>(pair);
            this._elements.Add(key, node);
            this._priority.AddFirst(node);
        }

        private void RemoveOldestElement()
        {
            var node = this._priority.Last;
            this._elements.Remove(node.Value.Key);
            this._priority.RemoveLast();
        }
    }
}
