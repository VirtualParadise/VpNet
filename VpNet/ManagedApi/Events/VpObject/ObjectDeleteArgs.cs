namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.ObjectDeleted" />.
    /// </summary>
    public sealed class ObjectDeleteArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectDeleteArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar responsible for the deletion.</param>
        /// <param name="obj">The deleted object.</param>
        public ObjectDeleteArgs(Avatar avatar, VpObject obj)
        {
            Avatar = avatar;
            Object = obj;
        }

        /// <summary>
        ///     Gets the avatar responsible for the deletion.
        /// </summary>
        /// <value>The avatar responsible for the deletion.</value>
        public Avatar Avatar { get; }

        /// <summary>
        ///     Gets the deleted object. 
        /// </summary>
        /// <value>The deleted object.</value>
        public VpObject Object { get; }
    }
}
