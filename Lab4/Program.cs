using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            const int handle = 0;
            var osHandle = new OSHandle(new IntPtr(handle));
        }
    }
}