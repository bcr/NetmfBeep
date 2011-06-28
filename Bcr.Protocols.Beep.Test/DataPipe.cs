using System;
using System.Threading;

namespace Bcr.Protocols.Beep.Test
{
    class DataPipe : IDataSink, IDataSource
    {
        private readonly byte[] _buffer = new byte[1024];
        private int _writeOffset;
        private int _readOffset;
        private int _length;
        private readonly AutoResetEvent _readDataAvailable = new AutoResetEvent(false);

        public int AddBytes(byte[] bytes, int offset, int length)
        {
            lock (_buffer)
            {
                Array.Copy(bytes, offset, _buffer, _writeOffset, length);
                _writeOffset += length;
                _length += length;
            }
            _readDataAvailable.Set();
            return length;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public bool IsDataCompleted
        {
            get { throw new NotImplementedException(); }
        }

        public int Read(byte[] buffer, int offset, int length)
        {
            var bytesToRead = 0;

            while (bytesToRead == 0)
            {
                _readDataAvailable.WaitOne(Timeout.Infinite);
                lock (_buffer)
                {
                    bytesToRead = (length < _length) ? length : _length;
                    if (bytesToRead > 0)
                    {
                        Array.Copy(_buffer, _readOffset, buffer, offset, bytesToRead);
                        _readOffset += bytesToRead;
                        _length -= bytesToRead;
                        Compact();
                    }
                }
            }
            return bytesToRead;
        }

        private void Compact()
        {
            lock (_buffer)
            {
                Array.Copy(_buffer, _readOffset, _buffer, 0, _length);
                _writeOffset -= _readOffset;
                _readOffset = 0;
            }
        }
    }
}
