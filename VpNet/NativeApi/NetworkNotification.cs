using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
