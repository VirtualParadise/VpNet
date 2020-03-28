namespace VpNet
{
    public enum DisconnectType
    {
        /// <summary>
        /// Indicates the universe server disconnected unexpectedly. (default)
        /// </summary>
        ServerDisconnected,
        /// <summary>
        /// Indicates the user disconnected from universe manually.
        /// </summary>
        UserDisconnected,
    }
}