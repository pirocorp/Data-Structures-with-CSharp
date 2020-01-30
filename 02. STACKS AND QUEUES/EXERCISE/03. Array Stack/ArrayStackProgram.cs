namespace _03._Array_Stack
{
    using System;

    public static class ArrayStackProgram
    {
        public static void Main()
        {
            var stack = new ArrayStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            Console.WriteLine(string.Join(", ", stack.ToArray()));

            var x = stack.Pop();
            Console.WriteLine(x);
            Console.WriteLine(string.Join(", ", stack.ToArray()));
            var y = stack.Pop();
            Console.WriteLine(y);
            Console.WriteLine(string.Join(", ", stack.ToArray()));
        }
    }
}