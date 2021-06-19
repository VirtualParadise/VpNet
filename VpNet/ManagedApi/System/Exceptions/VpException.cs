using System;
using System.Xml.Serialization;
using VpNet.NativeApi;

namespace VpNet
{
    /// <summary>
    ///     The exception that is thrown when an operation performed by the native SDK did not return
    ///     <see cref="ReasonCode.Success" />.
    /// </summary>
    [Serializable]
    [XmlRoot("VpException", Namespace = Global.XmlNsException)]
    public sealed class VpException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VpException" /> class.
        /// </summary>
        /// <param name="reason">The reason code.</param>
        public VpException(ReasonCode reason) : base($"VP SDK Error: {reason} ({(int) reason})")
        {
            Reason = reason;
        }

        /// <summary>
        ///     Gets the reason code.
        /// </summary>
        /// <value>The reason code.</value>
        public ReasonCode Reason { get; }
    }
}
