using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpNet.NativeApi
{
    internal enum NetworkReturnCode : int
    {
        Success = 0,
        ConnectionError = -1,
        WouldBlock = -2
    }
}
