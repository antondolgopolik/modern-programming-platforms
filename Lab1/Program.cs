using System;
using System.Threading;

namespace Lab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            TaskQueue taskQueue = new TaskQueue(5);
            for (int i = 0; i < 10; i++)
            {
                int j = i;
                taskQueue.EnqueueTask(delegate
                {
                    for (int k = j * 10; k < (j + 1) * 10; k++)
                    {
                        Console.Out.WriteLine("k = {0}", k);
                        Thread.Sleep(1000);
                    }
                });
            }

            taskQueue.Stop();
        }
    }
}