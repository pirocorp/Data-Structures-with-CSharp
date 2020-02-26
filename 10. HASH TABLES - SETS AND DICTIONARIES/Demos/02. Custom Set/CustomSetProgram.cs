namespace _02._Custom_Set
{
    using System;
    using System.Collections.Generic;

    public static class CustomSetProgram
    {
        public static void Main()
        {
            var hashSet = new HashSet<string>();

            hashSet.Add("Asen");
            hashSet.Add("Pesho");
            hashSet.Add("Gosho");
            hashSet.Add("Gergi");
            hashSet.Add("123");
            hashSet.Add("456");
            hashSet.Add("789");
            hashSet.Add("Vulcho");

            var testCollection = new HashSet<string>();
            testCollection.Add("123");
            testCollection.Add("456");
            testCollection.Add("789");
            testCollection.Add("Not Listed");
            testCollection.Add("Not Found");

            Console.WriteLine(string.Join(", ", hashSet));
            Console.WriteLine();
            Console.WriteLine(string.Join(", ", testCollection));
            Console.WriteLine();
            Console.WriteLine(string.Join(", ", hashSet.IntersectWith(testCollection)));
            Console.WriteLine();
            Console.WriteLine(string.Join(", ", hashSet.UnionWith(testCollection)));

            var n = -135;

            var y = n + (n >>= 31) ^ n;
            Console.WriteLine(y);
        }
    }
}