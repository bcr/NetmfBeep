using System;
using Microsoft.SPOT;

namespace Bcr.Protocols.Beep
{
    public class Frame : IDataSink
    {
        public Frame()
        {
            Header = new FrameHeader();
            Payload = new FramePayload();
            Trailer = new FrameTrailer();
            Reset();
        }

        public int AddBytes(byte[] bytes, int offset, int length)
        {
            int count = 0;

            if (!Header.IsDataCompleted)
            {
                count += Header.AddBytes(bytes, offset + count, length - count);
                if (Header.IsDataCompleted)
                {
                    Payload.Size = Header.Size;
                }
            }

            if ((Header.IsDataCompleted) && (!Payload.IsDataCompleted))
            {
                count += Payload.AddBytes(bytes, offset + count, length - count);
            }

            if ((Payload.IsDataCompleted) && (!Trailer.IsDataCompleted))
            {
                count += Trailer.AddBytes(bytes, offset + count, length - count);
            }

            return count;
        }

        public void Reset()
        {
            Header.Reset();
            Payload.Reset();
            Trailer.Reset();
        }

        public bool IsDataCompleted
        {
            get { return Trailer.IsDataCompleted; }
        }

        public FrameHeader Header { get; private set; }

        public FramePayload Payload { get; private set; }

        internal FrameTrailer Trailer { get; set; }
    }
}
