namespace _02._Sort_Words
{
    using System;
    using System.Linq;

    public static class Program
    {
        public static void Main()
        {
            var inputWords = Console.ReadLine()
                .Split(' ')
                .Where(x => x != "")
                .ToList();

            inputWords.Sort();

            Console.WriteLine(string.Join(" ", inputWords));
        }
    }
}