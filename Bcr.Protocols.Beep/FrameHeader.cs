using System;

namespace Bcr.Protocols.Beep
{
    public class FrameHeader : CrLfDataSink
    {
        public new int AddBytes(byte[] bytes, int offset, int length)
        {
            var returnValue = base.AddBytes(bytes, offset, length);
            if (IsDataCompleted)
            {
                Parse();
            }
            return returnValue;
        }

        private void Parse()
        {
            var strings = FinalString.Split(' ');

            ThisFrameType = Util.FrameTypeFromString(strings[0]);
            Channel = Int32.Parse(strings[1]);
            MessageNo = Int32.Parse(strings[2]);
            More = Util.IsMoreIndicator(strings[3]);
            SequenceNo = UInt32.Parse(strings[4]);
            Size = Int32.Parse(strings[5]);
            if (ThisFrameType == FrameType.Answer)
            {
                AnswerNo = Int32.Parse(strings[6]);
            }
        }

        public FrameType ThisFrameType { get; private set; }

        public int Channel { get; private set; }

        public int MessageNo { get; private set; }

        public bool More { get; private set; }

        public uint SequenceNo { get; private set; }

        public int Size { get; private set; }

        public int AnswerNo { get; set; }
    }
}
