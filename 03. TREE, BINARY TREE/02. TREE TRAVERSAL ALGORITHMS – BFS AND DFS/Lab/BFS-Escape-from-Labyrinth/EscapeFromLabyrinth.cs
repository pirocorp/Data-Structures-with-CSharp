using System;
using System.Collections.Generic;
using System.Text;

public class EscapeFromLabyrinth
{
    private const char VisitedCell = 's';

    private static int width = 9;

    private static int height = 7;

    private static char[,] labyrinth =
    {
        { '*', '*', '*', '*', '*', '*', '*', '*', '*', },
        { '*', '-', '-', '-', '-', '*', '*', '-', '-', },
        { '*', '*', '-', '*', '-', '-', '-', '-', '*', },
        { '*', '-', '-', '*', '-', '*', '-', '*', '*', },
        { '*', 's', '*', '-', '-', '*', '-', '*', '*', },
        { '*', '*', '-', '-', '-', '-', '-', '-', '*', },
        { '*', '*', '*', '*', '*', '*', '*', '-', '*', },
    };

    public static void Main()
    {
        ReadLabyrinth();

        var result = FindShortestPathToExit();

        if (result == null)
        {
            Console.WriteLine("No exit!");
        }
        else if (result == "")
        {
            Console.WriteLine("Start is at the exit.");
        }
        else
        {
            Console.WriteLine($"Shortest exit: {result}");
        }
    }

    private static void ReadLabyrinth()
    {
        width = int.Parse(Console.ReadLine());
        height = int.Parse(Console.ReadLine());
        labyrinth = new char[height, width];

        for (var row = 0; row < height; row++)
        {
            var inputRow = Console.ReadLine();

            for (var col = 0; col < width; col++)
            {
                labyrinth[row, col] = inputRow[col];
            }
        }
    }

    private static string FindShortestPathToExit()
    {
        var queue = new Queue<Point>();
        var startPosition = FindStartPosition();

        if (startPosition == null)
        {
            //No start position -> no exit
            return null;
        }

        queue.Enqueue(startPosition);

        while (queue.Count > 0)
        {
            var currentCell = queue.Dequeue();
            //Console.WriteLine($"Visited cell: ({currentCell.X}, {currentCell.Y})");

            if (IsExit(currentCell))
            {
                return TracePathBack(currentCell);
            }

            TryDirection(queue, currentCell, "U", 0, -1);
            TryDirection(queue, currentCell, "R", +1, 0);
            TryDirection(queue, currentCell, "D", 0, +1);
            TryDirection(queue, currentCell, "L", -1, 0);
        }

        return null;
    }

    private static string TracePathBack(Point currentCell)
    {
        var path = new StringBuilder();

        while (currentCell.PreviousPoint != null)
        {
            path.Append(currentCell.Direction);
            currentCell = currentCell.PreviousPoint;
        }

        var pathReversed = new StringBuilder(path.Length);

        for (var i = path.Length - 1; i >= 0; i--)
        {
            pathReversed.Append(path[i]);
        }

        return pathReversed.ToString();
    }

    private static void TryDirection(Queue<Point> queue, Point currentCell, 
        string direction, int deltaX, int deltaY)
    {
        var newX = currentCell.X + deltaX;
        var newY = currentCell.Y + deltaY;

        if (newX >= 0 && newX < width &&
            newY >= 0 && newY < height &&
            labyrinth[newY, newX] == '-')
        {
            labyrinth[newY, newX] = VisitedCell;

            var nextCell = new Point(newX, newY, direction, currentCell);
            queue.Enqueue(nextCell);
        }
    }

    private static bool IsExit(Point currentCell)
    {
        var isOnBorderX = currentCell.X == 0 || currentCell.X == width - 1;
        var isOnBorderY = currentCell.Y == 0 || currentCell.Y == height - 1;
        return isOnBorderX || isOnBorderY;
    }

    private static Point FindStartPosition()
    {
        for (var x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (labyrinth[y, x] == VisitedCell)
                {
                    return new Point(x, y);
                }
            }
        }

        return null;
    }

    private class Point
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(int x, int y, string direction, Point previousPoint) : this(x, y)
        {
            Direction = direction;
            PreviousPoint = previousPoint;
        }

        public string Direction { get; set; }

        public Point PreviousPoint { get; set; }
    }
}
