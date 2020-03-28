namespace VpNet.NativeApi
{
    internal enum NetworkNotification : int
    {
        Connect,
        Disconnect,
        ReadReady,
        WriteReady,
        Timeout
    }
}
