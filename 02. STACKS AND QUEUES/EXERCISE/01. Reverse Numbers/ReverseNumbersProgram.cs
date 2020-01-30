namespace _01._Reverse_Numbers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ReverseNumbersProgram
    {
        public static void Main()
        {
            var inputNumbers = Console.ReadLine()
                .Split(' ')
                .Where(x => x != "")
                .Select(int.Parse)
                .ToArray();

            var stack = new Stack<int>();

            foreach (var num in inputNumbers)
            {
                stack.Push(num);
            }

            Console.WriteLine(string.Join(" ", stack));
        }
    }
}