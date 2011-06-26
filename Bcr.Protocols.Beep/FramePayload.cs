using System;
using System.Text;

namespace Bcr.Protocols.Beep
{
    public class FramePayload : IDataSink
    {
        private int _dataOffset;
        private int _size;

        public int AddBytes(byte[] bytes, int offset, int length)
        {
            var bytesToCopy = (length < (Size - _dataOffset)) ? length : (Size - _dataOffset);
            Array.Copy(bytes, offset, Data, _dataOffset, bytesToCopy);
            _dataOffset += bytesToCopy;

            return bytesToCopy;
        }

        public void Reset()
        {
            Data = null;
        }

        public bool IsDataCompleted
        {
            get { return _dataOffset == Size; }
        }

        public int Size
        {
            get { return _size; }
            internal set
            {
                Data = new byte[value];
                _dataOffset = 0;
                _size = value;
            }
        }

        public byte[] Data { get; private set; }
    }
}
