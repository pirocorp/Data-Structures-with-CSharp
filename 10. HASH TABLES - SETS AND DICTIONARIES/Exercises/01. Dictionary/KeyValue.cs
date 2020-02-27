namespace _01._Dictionary
{
    public class KeyValue<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public KeyValue(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            var element = (KeyValue<TKey, TValue>)other;
            var equals = Equals(this.Key, element.Key) && Equals(this.Value, element.Value);
            return equals;
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        public override string ToString()
        {
            return $" [{this.Key} -> {this.Value}]";
        }
    }
}