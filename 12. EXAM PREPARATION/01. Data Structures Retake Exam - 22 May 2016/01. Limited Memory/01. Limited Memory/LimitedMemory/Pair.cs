namespace LimitedMemory
{
    public class Pair<TKey, TValue>
    {
        public Pair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TKey Key { get; private set; }

        public TValue Value { get; set; }

        public override string ToString()
        {
            return $"{this.Key} {this.Value}";
        }
    }
}
