namespace _02.Count_Symbols
{
    using System;
    using System.Collections.Generic;

    public static class CountSymbolsProgram
    {
        public static void Main()
        {
            var dict = new SortedDictionary<char, int>();

            var input = Console.ReadLine();

            if (input == null)
            {
                return;
            }

            foreach (var @char in input)
            {
                if (!dict.ContainsKey(@char))
                {
                    dict[@char] = 0;
                }

                dict[@char]++;
            }

            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} time/s");
            }
        }
    }
}