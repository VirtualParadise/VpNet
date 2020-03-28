using System;

namespace VpNet
{
    /// <summary>
    /// World state types.
    /// </summary>
    [Serializable]
    public enum WorldState
    {
        /// <summary>
        /// World Server is online
        /// </summary>
        Online,
        /// <summary>
        /// World Server is stopped
        /// </summary>
        Stopped,
        /// <summary>
        /// world Server is in an Unknown status
        /// </summary>
        Unknown
    }
}