namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.ObjectChanged" />.
    /// </summary>
    public sealed class ObjectChangeArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectChangeArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar responsible for the change.</param>
        /// <param name="obj">The changed object.</param>
        public ObjectChangeArgs(Avatar avatar, VpObject obj)
        {
            Avatar = avatar;
            Object = obj;
        }

        /// <summary>
        ///     Gets the avatar responsible for the change.
        /// </summary>
        /// <value>The avatar responsible for the change.</value>
        public Avatar Avatar { get; }
        
        /// <summary>
        ///     Gets the changed object. 
        /// </summary>
        /// <value>The changed object.</value>
        public VpObject Object { get; }
    }
}
