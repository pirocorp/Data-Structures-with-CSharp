namespace _03._Ride_the_Horse
{
    using System;
    using System.Collections.Generic;

    public static class RideTheHorseProgram
    {
        private static int rowsCount;

        private static int colsCount;

        public static void Main()
        {
            rowsCount = int.Parse(Console.ReadLine());
            colsCount = int.Parse(Console.ReadLine());

            var horseStartRow = int.Parse(Console.ReadLine());
            var horseStartCol = int.Parse(Console.ReadLine());

            var matrix = InitializeMatrix(rowsCount, colsCount);
            var startCell = new Cell(horseStartRow, horseStartCol, 1);

            BFS(startCell, matrix);
            PrintMatrix(matrix);
        }

        private static void PrintMatrix(int[][] matrix)
        {
            for (var i = 0; i < matrix.Length; i++)
            {
                Console.WriteLine(string.Join(" ", matrix[i]));
            }
        }

        private static void BFS(Cell startCell, int[][] matrix)
        {
            var queue = new Queue<Cell>();
            queue.Enqueue(startCell);

            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();
                matrix[currentCell.Row][currentCell.Col] = currentCell.Step;
                EnqueueAllNonVisitedCells(currentCell, matrix, queue);
            }
        }

        private static void EnqueueAllNonVisitedCells(Cell currentCell, int[][] matrix, Queue<Cell> queue)
        {
            if (currentCell.Row - 2 >= 0 && currentCell.Col - 1 >= 0 &&
                matrix[currentCell.Row - 2][currentCell.Col - 1] == 0)
            {
                var newCell = new Cell(currentCell.Row - 2, currentCell.Col - 1, currentCell.Step + 1);
                queue.Enqueue(newCell);
            }

            if (currentCell.Row - 1 >= 0 && currentCell.Col - 2 >= 0 &&
                matrix[currentCell.Row - 1][currentCell.Col - 2] == 0)
            {
                var newCell = new Cell(currentCell.Row - 1, currentCell.Col - 2, currentCell.Step + 1);
                queue.Enqueue(newCell);
            }

            if (currentCell.Row + 2 < rowsCount && currentCell.Col + 1 < colsCount &&
                matrix[currentCell.Row + 2][currentCell.Col + 1] == 0)
            {
                var newCell = new Cell(currentCell.Row + 2, currentCell.Col + 1, currentCell.Step + 1);
                queue.Enqueue(newCell);
            }

            if (currentCell.Row + 1 < rowsCount && currentCell.Col + 2 < colsCount &&
                matrix[currentCell.Row + 1][currentCell.Col + 2] == 0)
            {
                var newCell = new Cell(currentCell.Row + 1, currentCell.Col + 2, currentCell.Step + 1);
                queue.Enqueue(newCell);
            }

            if (currentCell.Row - 2 >= 0 && currentCell.Col + 1 < colsCount &&
                matrix[currentCell.Row - 2][currentCell.Col + 1] == 0)
            {
                var newCell = new Cell(currentCell.Row - 2, currentCell.Col + 1, currentCell.Step + 1);
                queue.Enqueue(newCell);
            }

            if (currentCell.Row - 1 >= 0 && currentCell.Col + 2 < colsCount &&
                matrix[currentCell.Row - 1][currentCell.Col + 2] == 0)
            {
                var newCell = new Cell(currentCell.Row - 1, currentCell.Col + 2, currentCell.Step + 1);
                queue.Enqueue(newCell);
            }

            if (currentCell.Row + 2 < rowsCount && currentCell.Col - 1 >= 0 &&
                matrix[currentCell.Row + 2][currentCell.Col - 1] == 0)
            {
                var newCel = new Cell(currentCell.Row + 2, currentCell.Col - 1, currentCell.Step + 1);
                queue.Enqueue(newCel);
            }

            if (currentCell.Row + 1 < rowsCount && currentCell.Col - 2 >= 0 &&
                matrix[currentCell.Row + 1][currentCell.Col - 2] == 0)
            {
                var newCel = new Cell(currentCell.Row + 1, currentCell.Col - 2, currentCell.Step + 1);
                queue.Enqueue(newCel);
            }
        }

        private static int[][] InitializeMatrix(int rowsCount, int colsCount)
        {
            var matrix = new int[rowsCount][];

            for (var row = 0; row < rowsCount; row++)
            {
                matrix[row] = new int[colsCount];

                for (var col = 0; col < colsCount; col++)
                {
                    matrix[row][col] = 0;
                }
            }

            return matrix;
        }

        private class Cell
        {
            public int Row { get; private set; }

            public int Col { get; private set; }

            public int Step { get; private set; }

            public Cell(int row, int col, int step)
            {
                this.Row = row;
                this.Col = col;
                this.Step = step;
            }

            public override string ToString()
            {
                return $"Row:{this.Row}, Col:{this.Col}, Step:{this.Step}";
            }
        }
    }
}