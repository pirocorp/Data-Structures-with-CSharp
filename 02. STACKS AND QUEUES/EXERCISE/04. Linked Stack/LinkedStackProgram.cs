namespace _04.Linked_Stack
{
    using System;

    public static class LinkedStackProgram
    {
        public static void Main()
        {
            var stack = new LinkedStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);

            Console.WriteLine(string.Join(", ", stack.ToArray()));

            var r = stack.Pop();
            Console.WriteLine(r);
            Console.WriteLine(string.Join(", ", stack.ToArray()));

            r = stack.Pop();
            Console.WriteLine(r);
            Console.WriteLine(string.Join(", ", stack.ToArray()));
        }
    }
}