namespace _03._Dictionary
{
    using System;

    public static class DictionaryProgram
    {
        public static void Main()
        {
            var dict = new Dictionary<string, int>();

            for (var i = 0; i < 101; i++)
            {
                dict.Add($"Key {i}", i);
            }

            Console.WriteLine(dict.Contains("Key 99"));
            Console.WriteLine(dict.GetValue("Key 99"));
            dict.Remove("Key 99");
            Console.WriteLine(dict.Contains("Key 99"));
            Console.WriteLine(dict.Count);

            Console.WriteLine(string.Join(", ", dict.Values()));
            Console.WriteLine(string.Join(", ", dict.Keys()));
        }
    }
}