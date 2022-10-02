using System.Net;

namespace VpNet
{
    /// <summary>
    ///     Represents a universe.
    /// </summary>
    public class Universe
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Universe" /> class.
        /// </summary>
        /// <param name="remoteEP">The remote endpoint of the universe.</param>
        public Universe(EndPoint remoteEP)
        {
            RemoteEndPoint = remoteEP;
        }

        /// <summary>
        ///     Gets the remote endpoint of the universe.
        /// </summary>
        /// <value>The remote endpoint.</value>
        public EndPoint RemoteEndPoint { get; set; }
    }
}