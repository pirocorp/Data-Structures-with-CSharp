using System;

class Launcher
{
    public static void Main()
    {
        var list = new LinkedList<int>();
        list.AddFirst(1);
        list.AddLast(2);
        list.AddLast(3);

        list.RemoveLast();

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }
}