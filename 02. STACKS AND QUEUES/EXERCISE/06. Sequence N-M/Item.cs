namespace _06._Sequence_N_M
{
    public class Item<T>
    {
        public T Value { get; }

        public Item<T> PrevItem { get; }

        public Item(T value, Item<T> prevItem)
        {
            this.Value = value;
            this.PrevItem = prevItem;
        }
    }
}