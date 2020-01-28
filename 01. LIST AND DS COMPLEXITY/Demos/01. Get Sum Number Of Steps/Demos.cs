namespace _01._Get_Sum_Number_Of_Steps
{
    using System;
    using System.Collections.Generic;

    public class Demos
    {
        public static void Main()
        {
            //var primes = FindPrimesInRange(100, 200);
            //Console.WriteLine(string.Join(", ", primes));
        }
        
        public static List<int> FindPrimesInRange(int start, int end)
        {
            var primes = new List<int>();

            for (var num = start; num <= end; num++)
            {
                var isPrime = true;
                var maxDivider = Math.Sqrt(num);

                for (var divider = 2; divider <= maxDivider; divider++)
                {
                    if (num % divider == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if(isPrime) primes.Add(num);
            }

            return primes;
        }

        //9n + 3 Steps
        private static int GetSumEven(int[] array)
        {
            var sum = 0; //1 Operation

            //i = 0 -> 1 operation, i < array.Length -> 1 operation
            for (var i = 0; i < array.Length; i++)
            {
                //array[i] -> 1 operation, % 2 -> 1 operation, == 0 -> 1 operation
                if (array[i] % 2 == 0)
                {
                    sum += array[i]; // sum += -> 2 operations array[i] -> 1 operation
                }
            } //i++ -> 2 operation, i < array.Length -> 1 operation

            return sum;
        }

        private static bool Contains(int[] array, int element)
        {
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    return true;
                }
            }

            return false;
        }

        //1.6 ^ n Complexity
        private static int Fibonacci(int n)
        {
            if (n == 0) return 1;
            else if (n == 1) return 1;
            else return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }
}