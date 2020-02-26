namespace _01._Hash_Table
{
    using System;

    public static class HashTableProgram
    {
        public static void Main()
        {
            var table = new HashTable<string, int>(2);
            table.Add("Asen", 123);
            //Console.WriteLine("Asen".GetHashCode());
            table.Add("Gosho", 123);
            //Console.WriteLine("Gosho".GetHashCode());
            table.Add("Petar", 123);
            //Console.WriteLine("Petar".GetHashCode());
            table.Add("Pesho", 123);
            //Console.WriteLine("Pesho".GetHashCode());

            for (int i = 0; i < 1000; i++)
            {
                table.Add("key" + i, i);
            }

            var kvp = table.Find("key0");
            var kvp2 = table.Find("key1000");

            Console.WriteLine(table.Get("Asen"));

            Console.WriteLine(table.ContainsKey("key1000"));

            if (table.TryGetValue("key500", out var value))
            {
                Console.WriteLine(value);
            }

            Console.WriteLine(string.Join(", ", table.Values));
            Console.WriteLine(string.Join(", ", table.Keys));

            Console.WriteLine(table.AddOrReplace("key100", 555_555));
            Console.WriteLine(table["key100"]);

            Console.WriteLine(table.Remove("key99"));
            Console.WriteLine(table.ContainsKey("key99"));

            table.Clear();
        }
    }
}