namespace _07._Distance_in_Labyrinth
{
    using System;
    using System.Linq;

    public static class Program
    {
        public static void Main()
        {
            var labyrinth = ReadLabyrinthFromConsole();

            for (var rowIndex = 0; rowIndex < labyrinth.Length; rowIndex++)
            {
                for (var colIndex = 0; colIndex < labyrinth[rowIndex].Length; colIndex++)
                {
                    if (labyrinth[rowIndex][colIndex] == "*")
                    {
                        FindLength(labyrinth, rowIndex, colIndex, 0);
                        break;
                    }
                }
            }

            for (var rowIndex = 0; rowIndex < labyrinth.Length; rowIndex++)
            {
                for (var colIndex = 0; colIndex < labyrinth[rowIndex].Length; colIndex++)
                {
                    if (labyrinth[rowIndex][colIndex] == "0")
                    {
                        labyrinth[rowIndex][colIndex] = "u";
                    }
                }
            }


            for (var i = 0; i < labyrinth.Length; i++)
            {
                Console.WriteLine(string.Join("", labyrinth[i]));
            }
        }

        private static void FindLength(string[][] labyrinth, int rowIndex, int colIndex, int currentLength)
        {
            if (currentLength != 0)
            {
                var isNumber = int.TryParse(labyrinth[rowIndex][colIndex], out var labyrinthLength);

                if (isNumber && (labyrinthLength > currentLength || labyrinthLength == 0))
                {
                    labyrinth[rowIndex][colIndex] = currentLength.ToString();
                }
                else
                {
                    return;
                }
            }

            currentLength++;

            if (rowIndex - 1 >= 0) FindLength(labyrinth, rowIndex - 1, colIndex, currentLength);
            if (rowIndex + 1 < labyrinth.Length) FindLength(labyrinth, rowIndex + 1, colIndex, currentLength);
            if(colIndex - 1 >= 0) FindLength(labyrinth, rowIndex, colIndex - 1, currentLength);
            if(colIndex + 1 < labyrinth.Length) FindLength(labyrinth, rowIndex, colIndex + 1, currentLength);
        }

        private static string[][] ReadLabyrinthFromConsole()
        {
            var n = int.Parse(Console.ReadLine());
            var matrix = new string[n][];

            for (var i = 0; i < n; i++)
            {
                var inputRow = Console.ReadLine()
                    .ToCharArray()
                    .Select(x => new string(x, 1))
                    .ToArray();

                matrix[i] = inputRow;
            }

            return matrix;
        }
    }
}