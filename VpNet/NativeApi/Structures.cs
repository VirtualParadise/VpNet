using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
