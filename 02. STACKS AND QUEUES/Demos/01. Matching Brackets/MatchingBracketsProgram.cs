namespace _01._Matching_Brackets
{
    using System;
    using System.Collections.Generic;

    public static class MatchingBracketsProgram
    {
        public static void Main()
        {
            var inputString = "1 + (2 - (2 + 3) * 4 / (3 + 1)) * 5";
            var stack = new Stack<int>();

            for (var i = 0; i < inputString.Length; i++)
            {
                var currentChar = inputString[i];

                if (currentChar == '(')
                {
                    stack.Push(i);
                }

                if (currentChar == ')')
                {
                    var startIndex = stack.Pop();
                    var endIndex = i;
                    var length = endIndex - startIndex + 1;
                    var subString = inputString.Substring(startIndex, length);

                    Console.WriteLine(subString);
                }
            }
        }
    }
}