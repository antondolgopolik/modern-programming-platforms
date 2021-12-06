using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6
{
    public class LogBuffer : IDisposable
    {
        private readonly StreamWriter _writer;
        private readonly ConcurrentQueue<string> _buffer;
        private readonly int _bufferSize;
        private readonly Timer _timer;
        private bool _isDisposed;

        public LogBuffer(string path, int bufferSize, int period)
        {
            _writer = new StreamWriter(path, true);
            _buffer = new ConcurrentQueue<string>();
            _bufferSize = bufferSize;
            _timer = new Timer(_ => Flush(), null, period, period);
        }

        ~LogBuffer()
        {
            Dispose();
        }

        public void Add(string item)
        {
            _buffer.Enqueue(item);
            if (_buffer.Count >= _bufferSize)
            {
                new Task(Flush).Start();
            }
        }

        private void Flush()
        {
            var lockTaken = false;
            try
            {
                Monitor.TryEnter(_writer, ref lockTaken);
                if (lockTaken)
                {
                    if (_isDisposed)
                    {
                        throw new ObjectDisposedException("LogBuffer");
                    }
                    
                    while (!_buffer.IsEmpty)
                    {
                        _buffer.TryDequeue(out var message);
                        _writer.WriteLine(message);
                    }

                    _writer.Flush();
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_writer);
                }
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _timer.Dispose();
                Flush();
                lock (_writer)
                {
                    _writer.Close();
                    _isDisposed = true;
                }
            }
        }
    }
}