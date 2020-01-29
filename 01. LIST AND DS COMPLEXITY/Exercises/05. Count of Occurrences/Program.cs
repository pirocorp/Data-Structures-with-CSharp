namespace _05._Count_of_Occurrences
{
    using System;
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

            var occurrences = new int[1001];

            for (var i = 0; i < inputNumbers.Count; i++)
            {
                var currentNumber = inputNumbers[i];
                occurrences[currentNumber]++;
            }

            for (var currentNumber = 0; currentNumber < occurrences.Length; currentNumber++)
            {
                if (occurrences[currentNumber] > 0)
                {
                    Console.WriteLine($"{currentNumber} -> {occurrences[currentNumber]} times");
                }
            }
        }
    }
}