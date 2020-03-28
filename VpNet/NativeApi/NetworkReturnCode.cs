namespace VpNet.NativeApi
{
    internal enum NetworkReturnCode : int
    {
        Success = 0,
        ConnectionError = -1,
        WouldBlock = -2
    }
}
