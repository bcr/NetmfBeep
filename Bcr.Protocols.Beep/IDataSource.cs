namespace Bcr.Protocols.Beep
{
    public interface IDataSource
    {
        int Read(byte[] buffer, int offset, int length);
    }
}
