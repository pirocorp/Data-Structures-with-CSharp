namespace LimitedMemory
{
    using System.Collections.Generic;

    public interface ILimitedMemoryCollection<TKey, TValue> : IEnumerable<Pair<TKey, TValue>>
    {
        int Capacity { get; }

        int Count { get; }

        void Set(TKey key, TValue value);

        TValue Get(TKey key);
    }
}
