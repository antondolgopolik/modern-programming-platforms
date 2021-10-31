using System.Collections.Generic;
using System.Threading;

namespace Lab1
{
    public class TaskQueue
    {
        public delegate void TaskDelegate();

        private readonly Queue<TaskDelegate> _tasks = new();
        private Thread[] _threadPool;
        private volatile bool isStopped;    

        public TaskQueue(int threadPoolSize)
        {
            _threadPool = new Thread[threadPoolSize];
            for (int i = 0; i < threadPoolSize; i++)
            {
                _threadPool[i] = new Thread(Run);
                _threadPool[i].Start();
            }
        }

        private void Run()
        {
            TaskDelegate task = null;
            var tasksArePresent = true;
            // Execute tasks
            while (!isStopped || tasksArePresent)
            {
                lock (_tasks)
                {
                    tasksArePresent = _tasks.Count != 0;
                    if (tasksArePresent)
                    {
                        task = _tasks.Dequeue();
                    }
                }

                if (task != null)
                {
                    task();
                    task = null;
                }
            }
        }

        public void EnqueueTask(TaskDelegate task)
        {
            lock (_tasks)
            {
                _tasks.Enqueue(task);
            }
        }

        public void Stop()
        {
            isStopped = true;
        }
    }
}