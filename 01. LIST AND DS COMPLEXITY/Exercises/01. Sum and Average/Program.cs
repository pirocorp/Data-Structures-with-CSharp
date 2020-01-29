namespace _01._Sum_and_Average
{
    using System;
    using System.Linq;

    public static class Program
    {
        public static void Main()
        {
            var inputNumbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries) //Only in .Core 
                .Select(int.Parse)
                .ToArray();

            decimal sum = inputNumbers.Sum();
            decimal average = 0;

            if (inputNumbers.Length > 0)
            {
                average = sum / inputNumbers.Length;
            }

            Console.WriteLine($"Sum={sum}; Average={average:F2}");
        }
    }
}