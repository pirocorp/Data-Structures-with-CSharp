namespace _01._Hash_Table
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int DEFAULT_CAPACITY = 16;
        private const float FILL_FACTOR = 0.75F;

        private int _maxElements;

        private LinkedList<KeyValue<TKey, TValue>>[] _hashTable;

        public int Count { get; private set; }

        public int Capacity => this._hashTable.Length;

        public IEnumerable<TKey> Keys => this.Select(x => x.Key);

        public IEnumerable<TValue> Values => this.Select(x => x.Value);

        public HashTable(int capacity = DEFAULT_CAPACITY)
        {
            this._hashTable = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this._maxElements = MaxElementsByFillFactor(capacity);
        }

        public void Add(TKey key, TValue value)
        {
            this.CheckGrowth();
            var hash = this.GetHashCode(key);

            if (this._hashTable[hash] == null)
            {
                this._hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            var kvp = new KeyValue<TKey, TValue>(key, value);

            foreach (var keyValue in this._hashTable[hash])
            {
                if (keyValue.Key.Equals(key))
                {
                    //Note: throw an exception on duplicate key
                    throw new ArgumentException("Duplicate Key:");
                }
            }

            this._hashTable[hash].AddLast(kvp);
            this.Count++;
        }

        private void CheckGrowth()
        {
            if (this.Count >= this._maxElements)
            {
                this.GrowTable();
            }
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            this.CheckGrowth();
            var hash = this.GetHashCode(key);

            if (this._hashTable[hash] == null)
            {
                this._hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var keyValue in this._hashTable[hash])
            {
                if (keyValue.Key.Equals(key))
                {
                    keyValue.Value = value;
                    return true;
                }
            }

            var kvp = new KeyValue<TKey, TValue>(key, value);

            this._hashTable[hash].AddLast(kvp);
            this.Count++;

            return false;
        }

        public TValue Get(TKey key)
        {
            var kvp = this.Find(key);

            if (kvp == null)
            {
                throw new KeyNotFoundException();
            }

            return kvp.Value;
        }

        public TValue this[TKey key]
        {
            get => this.Get(key);
            set => this.AddOrReplace(key, value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var kvp = this.Find(key);

            if (kvp == null)
            {
                value = default(TValue);
                return false;
            }

            value = kvp.Value;
            return true;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            var hash = this.GetHashCode(key);

            if (this._hashTable[hash] == null)
            {
                return null;
            }

            foreach (var kvp in this._hashTable[hash])
            {
                if (kvp.Key.Equals(key))
                {
                    return kvp;
                }
            }

            return null;
        }

        public bool ContainsKey(TKey key)
        {
            return this.Find(key) != null;
        }

        public bool Remove(TKey key)
        {
            var hash = this.GetHashCode(key);

            if (this._hashTable[hash] == null)
            {
                return false;
            }

            var kvp = this.Find(key);

            if (kvp == null)
            {
                return false;
            }

            this._hashTable[hash].Remove(kvp);
            this.Count--;

            return true;
        }

        public void Clear()
        {
            this._hashTable = new LinkedList<KeyValue<TKey, TValue>>[DEFAULT_CAPACITY];
            this.Count = 0;
            this._maxElements = MaxElementsByFillFactor(DEFAULT_CAPACITY);
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var linkedList in this._hashTable)
            {
                if (linkedList != null)
                {
                    foreach (var kvp in linkedList)
                    {
                        yield return kvp;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void GrowTable()
        {
            var newTable = new HashTable<TKey, TValue>(this.Capacity * 2);

            foreach (var linkedList in this._hashTable)
            {
                if (linkedList != null)
                {
                    foreach (var kvp in linkedList)
                    {
                        newTable.Add(kvp.Key, kvp.Value);
                    }
                }
            }

            this._hashTable = newTable._hashTable;
            this.Count = newTable.Count;
            this._maxElements = newTable._maxElements;
        }

        private static int MaxElementsByFillFactor(int capacity)
        {
            return (int)Math.Floor(capacity * FILL_FACTOR);
        }

        private int GetHashCode(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % this.Capacity;
        }
    }
}