using System;
using System.Runtime.InteropServices;

namespace Lab4
{
    public class OSHandle : IDisposable
    {
        [DllImport("Kernel32")]
        private static extern bool CloseHandle(IntPtr hObject);

        private readonly IntPtr _hObject;
        private bool _isDisposed;

        public OSHandle(IntPtr hObject)
        {
            if (hObject == IntPtr.Zero)
            {
                throw new ArgumentException("Pointer to handle can't be null");
            }

            _hObject = hObject;
        }

        ~OSHandle()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                CloseHandle(_hObject);
                _isDisposed = true;
            }
        }
    }
}