using System;
using System.Threading;
using static Lab7.Parallel;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskDelegate[] taskDelegates = new TaskDelegate[3];
            taskDelegates[0] = () => Run(0, 10, 100);
            taskDelegates[1] = () => Run(10, 20, 300);
            taskDelegates[2] = () => Run(20, 30, 500);
            WaitAll(taskDelegates);
            Console.WriteLine("Done");
        }

        static void Run(int start, int end, int timeout)
        {
            for (int i = start; i < end; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(timeout);
            }
        }
    }
}