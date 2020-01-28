namespace _02._ArrayList
{
    using System;

    public class Program
    {
        public static void Main()
        {
            var list = new ArrayList<int>(4);
            list.Add(10);
            list.Add(20);
            list.Add(30);
            list.Add(40);
            list.RemoveAt(3);
            list.RemoveAt(0);
            list.RemoveAt(0);
            list.RemoveAt(0);
            list.Add(5);
            list.Add(10);
            list.Add(15);
            list.Add(20);
        }
    }
}