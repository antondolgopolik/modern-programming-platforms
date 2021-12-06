using System;
using System.Threading;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            var logBuffer = new LogBuffer("/home/anton/Documents/log.txt", 10, 5000);
            for (int i = 0; i < 100; i++)
            {
                logBuffer.Add(i.ToString());
            }

            logBuffer.Dispose();
        }
    }
}