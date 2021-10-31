using System;
using System.Threading;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            var logBuffer = new LogBuffer("D:\\temp\\log.txt", 10, 5000);
            for (int i = 0; i < 5; i++)
            {
                logBuffer.Add(i.ToString());
            }
            
            Thread.Sleep(30000);
        }
    }
}