namespace _02._Calculate_Arithmetic_Expression
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public static class CalculateArithmeticExpressionProgram
    {
        private static readonly Queue<string> Output = new Queue<string>();

        private static readonly Stack<string> Operators = new Stack<string>();

        public static void Main()
        {
            var tokens = Console.ReadLine()
                ?.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine(string.Join(", ", tokens));

            try
            {
                ConvertExpressionToPostfix(tokens);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Parentheses Error");
                return;
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Not supported operation");
                return;
            }

            Console.WriteLine(string.Join(" ", Output.ToArray()));

            var result = EvaluateExpression(Output);
            Console.WriteLine(result);
        }

        private static decimal EvaluateExpression(Queue<string> input)
        {
            var result = new Stack<decimal>();

            while (input.Count > 0)
            {
                var current = input.Dequeue();
                var isNumber = decimal.TryParse(current, out var num);

                if (isNumber)
                {
                    result.Push(num);
                }
                else
                {
                    result.Push(ProcessOperation(current, result.Pop(), result.Pop()));
                }
            }

            return result.Pop();
        }

        private static decimal ProcessOperation(string current, decimal num1, decimal num2)
        {
            switch (current)
            {
                case "+":
                    return num2 + num1;
                case "-":
                    return num2 - num1;
                case "*":
                    return num2 * num1;
                case "/":
                    return num2 / num1;
                case "^":
                    return (long)num2 ^ (long)num1;
                default:
                    return 0;
            }
        }

        private static void ConvertExpressionToPostfix(string[] tokens)
        {
            for (var i = 0; i < tokens.Length; i++)
            {
                var isNumber = decimal.TryParse(tokens[i], System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture,
                    out var result);

                if (isNumber)
                {
                    Output.Enqueue(result.ToString());
                }
                else if (IsOperator(tokens[i]))
                {
                    var currentOperator = tokens[i];

                    while (Operators.Count > 0)
                    {
                        var topOperator = Operators.Peek();

                        if ((IsLeftAssociative(currentOperator) && IsLessThanOrEqualPrecedence(currentOperator, topOperator)) ||
                            (!IsLeftAssociative(currentOperator) && IsLeesThanPrecedence(currentOperator, topOperator)))
                        {
                            Output.Enqueue(Operators.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }

                    Operators.Push(currentOperator);
                }
                else if (IsLeftParenthesis(tokens[i]))
                {
                    Operators.Push(tokens[i]);
                }
                else if (IsRightParenthesis(tokens[i]))
                {
                    var isFound = false;

                    while (Operators.Count > 0 && !isFound)
                    {
                        if (IsLeftParenthesis(Operators.Peek()))
                        {
                            isFound = true;
                            Operators.Pop();
                        }
                        else
                        {
                            Output.Enqueue(Operators.Pop());
                        }
                    }

                    if (!isFound)
                    {
                        throw new ArgumentException();
                    }
                }
            }

            if (Operators.Contains("("))
            {
                throw new ArgumentException();
            }

            while (Operators.Count > 0)
            {
                Output.Enqueue(Operators.Pop());
            }
        }

        private static bool IsRightParenthesis(string token)
        {
            return token == ")";
        }

        private static bool IsLeftParenthesis(string token)
        {
            return token == "(";
        }

        private static bool IsLeesThanPrecedence(string operator1, string operator2)
        {
            return GetPrecedence(operator1) < GetPrecedence(operator2);
        }

        private static bool IsLessThanOrEqualPrecedence(string operator1, string operator2)
        {
            return GetPrecedence(operator1) <= GetPrecedence(operator2);
        }

        private static int GetPrecedence(string op)
        {
            switch (op)
            {
                case "+":
                    return 1;
                case "-":
                    return 1;
                case "*":
                    return 2;
                case "/":
                    return 2;
                case "^":
                    return 3;
                default:
                    throw new NotSupportedException();
            }
        }

        private static bool IsLeftAssociative(string currentOperator)
        {
            switch (currentOperator)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                    return true;
                case "^":
                    return false;
                default:
                    throw new NotSupportedException();
            }
        }

        private static bool IsOperator(string token)
        {
            switch (token)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                    return true;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
