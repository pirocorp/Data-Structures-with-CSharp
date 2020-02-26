namespace _03._Dictionary
{
    using System;
    using System.Collections.Generic;

    public interface IDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
    {
        int Count { get; }

        TValue this[TKey key] { get; set; }

        void Add(TKey key, TValue value);

        void Remove(TKey key);

        TValue GetValue(TKey key);

        void SetValue(TKey key, TValue value);

        bool Contains(TKey key);

        IEnumerable<TValue> Values();

        IEnumerable<TKey> Keys();
    }
}