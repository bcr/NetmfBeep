using System;

namespace Bcr.Protocols.Beep
{
    public class CrLfDataSink : IDataSink
    {
        private DataState _currentState;
        private string _finalString;

        enum DataState
        {
            None = 0,
            WaitingForCr,
            WaitingForLf,
            Done
        }

        public CrLfDataSink()
        {
            Reset();
        }

        public int AddBytes(byte[] bytes, int offset, int length)
        {
            int counter;

            for (counter = 0; counter < length; ++counter)
            {
                switch (_currentState)
                {
                    case DataState.WaitingForCr:
                        if (bytes[offset + counter] == '\r')
                        {
                            _currentState = DataState.WaitingForLf;
                        }
                        break;
                    case DataState.WaitingForLf:
                        _currentState = bytes[offset + counter] == '\n' ? DataState.Done : DataState.WaitingForCr;
                        break;
                    case DataState.Done:
                        return counter;
                }
                _finalString += ((char) bytes[offset + counter]);
            }

            return counter;
        }

        public void Reset()
        {
            _currentState = DataState.WaitingForCr;
            _finalString = "";
        }

        public bool IsDataCompleted { get { return _currentState == DataState.Done; } }
        public string FinalString { get { return _finalString; } }
    }
}
