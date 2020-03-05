using System;
using System.Linq;

public static class JudgeLauncher
{
    public static void Main()
    {
        var judge = new Judge();
        var x = judge.GetContests().ToArray();
        Console.WriteLine(x);
    }
}

