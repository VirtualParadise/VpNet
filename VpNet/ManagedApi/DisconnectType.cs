namespace VpNet
{
    /// <summary>
    ///     An enumeration of disconnect types.
    /// </summary>
    public enum DisconnectType
    {
        /// <summary>
        ///     Indicates the universe server disconnected unexpectedly. (Default)
        /// </summary>
        ServerDisconnected,
        
        /// <summary>
        ///     Indicates the user disconnected from universe manually.
        /// </summary>
        UserDisconnected,
    }
}