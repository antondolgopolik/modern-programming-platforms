using System;
using System.Threading;

namespace Lab7
{
    public static class Parallel
    {
        public delegate void TaskDelegate();

        public static void WaitAll(TaskDelegate[] taskDelegates)
        {
            var mre = new ManualResetEvent(false);
            var counter = new RefInt(taskDelegates.Length);
            foreach (var taskDelegate in taskDelegates)
            {
                var info = new Tuple<TaskDelegate, ManualResetEvent, RefInt>(taskDelegate, mre, counter);
                ThreadPool.QueueUserWorkItem(ThreadProc, info);
            }
            
            mre.WaitOne();
        }

        private static void ThreadProc(object infoParam)
        {
            var info = infoParam as Tuple<TaskDelegate, ManualResetEvent, RefInt>;
            info.Item1();
            if (Interlocked.Decrement(ref info.Item3.Val) == 0)
            {
                info.Item2.Set();
            }
        }

        private class RefInt
        {
            private int _val;

            public RefInt(int val)
            {
                _val = val;
            }

            public ref int Val => ref _val;
        }
    }
}