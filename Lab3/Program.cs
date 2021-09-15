using System;
using System.Threading;
using Lab1;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskQueue = new TaskQueue(10);
            var mutex = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                int j = i;
                taskQueue.EnqueueTask(delegate
                {
                    mutex.Lock();
                    for (int k = j * 10; k < (j + 1) * 10; k++)
                    {
                        Console.Out.WriteLine("k = {0}", k);
                        Thread.Sleep(1000);
                    }

                    mutex.Unlock();
                });
            }

            taskQueue.Stop();
        }
    }
}