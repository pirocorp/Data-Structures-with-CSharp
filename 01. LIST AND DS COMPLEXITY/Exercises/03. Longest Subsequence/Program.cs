namespace _03._Longest_Subsequence
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

            var longestSubsequence = GetLongestSubsequence(inputNumbers);
            Console.WriteLine(string.Join(" ", longestSubsequence));
        }

        private static List<int> GetLongestSubsequence(List<int> inputNumbers)
        {
            var result = new List<int>();

            if (inputNumbers.Count == 0)
            {
                return result;
            }

            var startIndex = 0;
            var maxLength = 0;

            for (var currentElementIndex = 0; currentElementIndex < inputNumbers.Count; currentElementIndex++)
            {
                var currentElement = inputNumbers[currentElementIndex];
                var currentLength = 1;

                for (var nextElementIndex = currentElementIndex + 1; nextElementIndex < inputNumbers.Count; nextElementIndex++)
                {
                    var nextElement = inputNumbers[nextElementIndex];

                    if (currentElement != nextElement)
                    {
                        break;
                    }

                    currentLength++;
                }

                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                    startIndex = currentElementIndex;
                }
            }

            for (var i = startIndex; i < startIndex + maxLength; i++)
            {
                result.Add(inputNumbers[i]);
            }

            return result;
        }
    }
}