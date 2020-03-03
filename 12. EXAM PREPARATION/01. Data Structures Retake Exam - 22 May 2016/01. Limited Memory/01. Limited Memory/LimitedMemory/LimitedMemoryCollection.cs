namespace LimitedMemory
{
    using System.Collections.Generic;
    using System.Collections;

    public class LimitedMemoryCollection<TKey, TValue> : ILimitedMemoryCollection<TKey, TValue>
    {
        private readonly Dictionary<TKey, Pair<TKey, TValue>> _byKey;
        private readonly Queue<Pair<TKey, TValue>> _byOrderOfInsertion;

        private readonly LinkedList<Pair<TKey, TValue>> _byMostRecentRequest;
        private readonly Dictionary<TKey, LinkedListNode<Pair<TKey, TValue>>> _byMostRecentByKey;
        private readonly int _capacity;

        public LimitedMemoryCollection(int capacity)
        {
            this._byKey = new Dictionary<TKey, Pair<TKey, TValue>>();
            this._byOrderOfInsertion = new Queue<Pair<TKey, TValue>>(capacity);

            this._byMostRecentRequest = new LinkedList<Pair<TKey, TValue>>();
            this._byMostRecentByKey = new Dictionary<TKey, LinkedListNode<Pair<TKey, TValue>>>();
            this._capacity = capacity;
        }

        public int Capacity => this._capacity;

        public int Count => this._byOrderOfInsertion.Count;

        public void Set(TKey key, TValue value)
        {
            var newPair = new Pair<TKey, TValue>(key, value);

            if (this._byKey.ContainsKey(key))
            {
                var current = this._byKey[key];
                current.Value = value;
                this.ChangeMostRecent(current);
                return;
            }

            this.AddToMostRecent(newPair);

            if (this.Capacity == this.Count)
            {
                var oldElement = this._byOrderOfInsertion.Dequeue();
                this._byKey.Remove(oldElement.Key);

                this._byKey.Add(key, newPair);
                this._byOrderOfInsertion.Enqueue(newPair);

                return;
            }

            this._byKey[key] = newPair;
            this._byOrderOfInsertion.Enqueue(newPair);
        }

        public TValue Get(TKey key)
        {
            if (!this._byKey.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            var current = this._byKey[key];
            this.ChangeMostRecent(current);
            return current.Value;
        }

        public IEnumerator<Pair<TKey, TValue>> GetEnumerator()
        {
            return this._byMostRecentRequest.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void AddToMostRecent(Pair<TKey, TValue> pair)
        {
            var current = this._byMostRecentRequest.AddFirst(pair);
            this._byMostRecentByKey.Add(pair.Key, current);
        }

        private void ChangeMostRecent(Pair<TKey, TValue> current)
        {
            var node = this._byMostRecentByKey[current.Key];

            this._byMostRecentRequest.Remove(node);
            this._byMostRecentRequest.AddFirst(node);
        }
    }
}
