namespace _01._Dictionary
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private HashTable<TKey, TValue> _hashTable;

        public Dictionary()
        {
            this._hashTable = new HashTable<TKey, TValue>();
        }

        public int Count => this._hashTable.Count;

        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get => this._hashTable.Get(key);
            set => this._hashTable.AddOrReplace(key, value);
        }

        public ICollection<TKey> Keys => this._hashTable.Keys.ToArray();
        public ICollection<TValue> Values => this._hashTable.Values.ToArray();

        public void Add(TKey key, TValue value)
        {
            this._hashTable.Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            // ReSharper disable once PossibleNullReferenceException
            //item is a struct and ReSharper is wrong in this case
            var (key, value) = item;

            this._hashTable.Add(key, value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this._hashTable.TryGetValue(key, out value);
        }

        public bool Remove(TKey key)
        {
            return this._hashTable.Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this._hashTable.Remove(item.Key);
        }

        public void Clear()
        {
            this._hashTable = new HashTable<TKey, TValue>();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var element = this._hashTable.Find(item.Key);

            if (element.Key.Equals(item.Key) && element.Value.Equals(item.Value))
            {
                return true;
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            return this._hashTable.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this._hashTable.ToArray().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this._hashTable
                .Select(element => new KeyValuePair<TKey, TValue>(element.Key, element.Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}