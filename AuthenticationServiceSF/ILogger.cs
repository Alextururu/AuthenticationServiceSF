namespace AuthenticationServiceSF
{
    public interface ILogger
    {
        void WriteEvent(string eventMessage);
        void WriteError(string eventError);
    }
}
