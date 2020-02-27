namespace _01._Dictionary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int INITIAL_CAPACITY = 16;
        private const float LOAD_FACTOR = 0.75F;

        private LinkedList<KeyValue<TKey, TValue>>[] _slots;

        public int Count { get; private set; }

        public HashTable(int capacity = INITIAL_CAPACITY)
        {
            this._slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public int Capacity => this._slots.Length;

        public void Add(TKey key, TValue value)
        {
            this.GrowIfNeeded();
            var slotNumber = this.FindSlotNumber(key);

            if (this._slots[slotNumber] == null)
            {
                this._slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this._slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException($"Key already exists: {key}");
                }
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);
            this._slots[slotNumber].AddLast(newElement);
            this.Count++;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            this.GrowIfNeeded();
            var slotNumber = this.FindSlotNumber(key);

            if (this._slots[slotNumber] == null)
            {
                this._slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this._slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    element.Value = value;
                    return false;
                }
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);
            this._slots[slotNumber].AddLast(newElement);
            this.Count++;
            return true;
        }

        public TValue Get(TKey key)
        {
            var element = this.Find(key);

            if (element == null)
            {
                throw new KeyNotFoundException();
            }

            return element.Value;
        }

        public TValue this[TKey key]
        {
            get => this.Get(key);
            set => this.AddOrReplace(key, value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = this.Find(key);

            if (element != null)
            {
                value = element.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            var slotNumber = this.FindSlotNumber(key);
            var bucket = this._slots[slotNumber];

            if (bucket == null)
            {
                return null;
            }

            foreach (var element in bucket)
            {
                if (element.Key.Equals(key))
                {
                    return element;
                }
            }

            return null;
        }

        public bool ContainsKey(TKey key)
        {
            var element = this.Find(key);
            return element != null;
        }

        public bool Remove(TKey key)
        {
            var slotNumber = this.FindSlotNumber(key);
            var bucket = this._slots[slotNumber];

            if (bucket == null)
            {
                return false;
            }

            var currentElement = this.Find(key);

            if (currentElement == null)
            {
                return false;
            }

            bucket.Remove(currentElement);
            this.Count--;
            return true;
        }

        public void Clear()
        {
            this._slots = new LinkedList<KeyValue<TKey, TValue>>[INITIAL_CAPACITY];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(x => x.Key);

        public IEnumerable<TValue> Values => this.Select(x => x.Value);

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var bucket in this._slots)
            {
                if (bucket == null)
                {
                    continue;
                }

                foreach (var element in bucket)
                {
                    yield return element;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void GrowIfNeeded()
        {
            var currentLoadFactor = (this.Count + 1) * 1.0F / this.Capacity;

            if (currentLoadFactor > LOAD_FACTOR)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            var newHashTable = new HashTable<TKey, TValue>(2 * this.Capacity);

            foreach (var element in this)
            {
                newHashTable.Add(element.Key, element.Value);
            }

            this._slots = newHashTable._slots;
            this.Count = newHashTable.Count;
        }

        private int FindSlotNumber(TKey key)
        {
            var slotNumber = Math.Abs(key.GetHashCode()) % this._slots.Length;
            return slotNumber;
        }
    }
}