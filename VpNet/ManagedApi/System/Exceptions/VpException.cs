using System;
using System.Xml.Serialization;
using VpNet.NativeApi;

namespace VpNet
{
    [Serializable]
    [XmlRoot("VpException", Namespace = Global.XmlNsException)]
    public sealed class VpException : Exception
    {
        public ReasonCode Reason;

        public VpException(ReasonCode reason) : base(string.Format("VP SDK Error: {0}({1})", reason, (int)reason))
        {
            Reason = reason;
        }

        public VpException() { }
    }
}
