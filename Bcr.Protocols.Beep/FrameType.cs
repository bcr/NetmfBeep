namespace Bcr.Protocols.Beep
{
    public enum FrameType
    {
        None = 0,
        Message,
        Reply,
        Answer,
        Error,
        Null
    }
}