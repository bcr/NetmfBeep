namespace Bcr.Protocols.Beep
{
    public interface IDataSink
    {
        int AddBytes(byte[] bytes, int offset, int length);
        void Reset();

        bool IsDataCompleted { get; }
    }
}