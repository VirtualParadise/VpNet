namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.OnObjectCreate" />.
    /// </summary>
    public sealed class ObjectCreateArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectCreateArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar responsible for the creation.</param>
        /// <param name="obj">The created object.</param>
        public ObjectCreateArgs(Avatar avatar, VpObject obj)
        {
            Avatar = avatar;
            Object = obj;
        }

        /// <summary>
        ///     Gets the avatar responsible for the creation.
        /// </summary>
        /// <value>The avatar responsible for the creation.</value>
        public Avatar Avatar { get; }
        
        /// <summary>
        ///     Gets the created object. 
        /// </summary>
        /// <value>The created object.</value>
        public VpObject Object { get; }
    }
}