namespace _03.Phonebook
{
    using System;
    using System.Collections.Generic;

    public static class PhonebookProgram
    {
        private static readonly Dictionary<string, string> Dictionary = new Dictionary<string, string>();

        public static void Main()
        {
            ReadInput();
            ProcessQueries();
        }

        private static void ProcessQueries()
        {
            var key = Console.ReadLine();

            while (!string.IsNullOrEmpty(key))
            {
                var hasKey = Dictionary.TryGetValue(key, out var number);

                if (hasKey)
                {
                    Console.WriteLine($"{key} -> {number}");
                }
                else
                {
                    Console.WriteLine($"Contact {key} does not exist.");
                }

                key = Console.ReadLine();
            }
        }

        private static void ReadInput()
        {
            var input = Console.ReadLine();

            while (input != "search")
            {
                var tokens = input.Split('-');

                var name = tokens[0];
                var number = tokens[1];

                Dictionary[name] = number;

                input = Console.ReadLine();
            }
        }
    }
}