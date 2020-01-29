namespace _08._Breadth_First_Search_Solution_For_Labyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Cell
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public int Step { get; set; }

        public Cell(int row, int col, int step)
        {
            this.Row = row;
            this.Col = col;
            this.Step = step;
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var labyrinth = ReadLabyrinthFromConsole();
            var startRow = 0; 
            var startCol = 0;
            GetStartingPosition(labyrinth, ref startRow, ref startCol);

            var queue = new Queue<Cell>();
            queue.Enqueue(new Cell(startRow, startCol, 0));

            BreadthFirstSearch(labyrinth, queue);

            FormattingMatrix(labyrinth);

            PrintMatrix(labyrinth);
        }
        
        private static void BreadthFirstSearch(string[][] labyrinth, Queue<Cell> queue)
        {
            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();

                if (currentCell.Row > 0 &&
                    labyrinth[currentCell.Row - 1][currentCell.Col] == "0")
                {
                    queue.Enqueue(new Cell(currentCell.Row - 1, currentCell.Col, currentCell.Step + 1));
                    labyrinth[currentCell.Row - 1][currentCell.Col] = (currentCell.Step + 1).ToString();
                }

                if (currentCell.Row + 1 < labyrinth.Length && 
                    labyrinth[currentCell.Row + 1][currentCell.Col] == "0")
                {
                    queue.Enqueue(new Cell(currentCell.Row + 1, currentCell.Col, currentCell.Step + 1));
                    labyrinth[currentCell.Row + 1][currentCell.Col] = (currentCell.Step + 1).ToString();
                }

                if (currentCell.Col > 0 &&
                    labyrinth[currentCell.Row][currentCell.Col - 1] == "0")
                {
                    queue.Enqueue(new Cell(currentCell.Row , currentCell.Col - 1, currentCell.Step + 1));
                    labyrinth[currentCell.Row][currentCell.Col - 1] = (currentCell.Step + 1).ToString();

                }

                if (currentCell.Col + 1 < labyrinth.Length &&
                    labyrinth[currentCell.Row][currentCell.Col + 1] == "0")
                {
                    queue.Enqueue(new Cell(currentCell.Row, currentCell.Col + 1, currentCell.Step + 1));
                    labyrinth[currentCell.Row][currentCell.Col + 1] = (currentCell.Step + 1).ToString();
                }

            }
        }

        private static void GetStartingPosition(string[][] labyrinth, ref int startRow, ref int startCol)
        {
            for (var rowIndex = 0; rowIndex < labyrinth.Length; rowIndex++)
            {
                for (var colIndex = 0; colIndex < labyrinth[rowIndex].Length; colIndex++)
                {
                    if (labyrinth[rowIndex][colIndex] == "*")
                    {
                        startRow = rowIndex;
                        startCol = colIndex;
                        break;
                    }
                }
            }
        }

        private static void FormattingMatrix(string[][] labyrinth)
        {
            for (var row = 0; row < labyrinth.Length; row++)
            {
                for (var col = 0; col < labyrinth.Length; col++)
                {
                    if (labyrinth[row][col] == "0") labyrinth[row][col] = "u";
                }
            }
        }

        private static void PrintMatrix(string[][] labyrinth)
        {
            for (var i = 0; i < labyrinth.Length; i++)
            {
                Console.WriteLine(string.Join("", labyrinth[i]));
            }
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