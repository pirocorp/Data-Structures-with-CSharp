namespace _04._Remove_Odd_Occurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Program
    {
        public static void Main()
        {
            var inputNumbers = Console.ReadLine()
                .Split(' ')
                .Where(x => x != "")
                .Select(int.Parse)
                .ToList();

            var numbers = RemoveOddOccurrences(inputNumbers);
            Console.WriteLine(string.Join(" ", numbers));
        }

        private static List<int> RemoveOddOccurrences(List<int> inputNumbers)
        {
            var result = new List<int>();

            if (inputNumbers.Count <= 1)
            {
                return result;
            }

            for (var i = 0; i < inputNumbers.Count; i++)
            {
                var currentElement = inputNumbers[i];
                var occurrences = inputNumbers.Count(x => x == currentElement);

                if (occurrences % 2 == 0)
                {
                    result.Add(currentElement);
                }
            }

            return result;
        }
    }
}