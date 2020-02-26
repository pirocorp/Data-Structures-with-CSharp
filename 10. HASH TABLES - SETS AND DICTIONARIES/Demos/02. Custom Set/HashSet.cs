namespace _02._Custom_Set
{
    using System.Collections;
    using System.Collections.Generic;
    using _01._Hash_Table;

    public class HashSet<T> : IEnumerable<T>
    {
        private readonly HashTable<T, T> _hashTable;

        public HashSet()
        {
            this._hashTable = new HashTable<T, T>();
        }

        public HashSet(IEnumerable<T> input) : this()
        {
            foreach (var el in input)
            {
                this.Add(el);
            }
        }

        public void Add(T item)
        {
            this._hashTable.AddOrReplace(item, item);
        }

        public IEnumerable<T> IntersectWith(IEnumerable<T> other)
        {
            var result = new HashSet<T>();

            foreach (var el in other)
            {
                if (this._hashTable.ContainsKey(el))
                {
                    result.Add(el);
                }
            }

            return result;
        }

        public IEnumerable<T> UnionWith(IEnumerable<T> other)
        {
            var result = new HashSet<T>(this);

            foreach (var el in other)
            {
                result.Add(el);
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._hashTable.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}