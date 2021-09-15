using System.Threading;

namespace Lab3
{
    public class Mutex
    {
        private int _isLocked;

        public void Lock()
        {
            while (Interlocked.CompareExchange(ref _isLocked, 1, 0) != 0) ;
        }

        public void Unlock()
        {
            while (Interlocked.CompareExchange(ref _isLocked, 0, 1) != 1) ;
        }
    }
}