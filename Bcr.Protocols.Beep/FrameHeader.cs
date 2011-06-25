using System;

namespace Bcr.Protocols.Beep
{
    public class FrameHeader : IDataSink
    {
        public int AddBytes(byte[] bytes, int offset, int length)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public bool IsDataCompleted
        {
            get { throw new NotImplementedException(); }
        }
    }
}
