namespace _04._Sequence_N
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class SequenceNProgram
    {
        public static void Main()
        {
            var queue = new Queue<int>();
            //var fullQueue = new Queue<int>();
            var stopwatch = new Stopwatch();

            var n = 7;
            var p = 100000000;

            if (n > p)
            {
                Console.WriteLine("Not possible");
                return;
            }

            if (n == p)
            {
                Console.WriteLine(1);
                Console.WriteLine(n);
                return;
            }

            queue.Enqueue(n);
            //fullQueue.Enqueue(n);

            var index = 1;
            var current = n;

            while (true)
            {
                stopwatch.Start();
                current = queue.Dequeue();

                var add = current + 1;
                index++;

                if (add == p)
                {
                    //fullQueue.Enqueue(add);
                    Console.WriteLine(index);
                    stopwatch.Stop();
                    break;
                }

                
                //fullQueue.Enqueue(add);
                var multiply = current * 2;
                index++;

                if (multiply == p)
                {
                    //fullQueue.Enqueue(multiply);
                    Console.WriteLine(index);
                    stopwatch.Stop();
                    break;
                }

                queue.Enqueue(add);
                queue.Enqueue(multiply);
                //fullQueue.Enqueue(multiply);
            }

            //Console.WriteLine(string.Join(", ", fullQueue));
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}