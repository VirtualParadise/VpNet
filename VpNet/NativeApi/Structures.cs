using System;
using System.Runtime.InteropServices;

namespace VpNet.NativeApi
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    struct NetConfig
    {
        public SocketCreateFunction Create;
        public SocketDestroyFunction Destroy;
        public SocketConnectFunction Connect;
        public SocketSendFunction Send;
        public SocketReceiveFunction Receive;
        public SocketTimeoutFunction Timeout;
        public IntPtr Context;
    }
}
