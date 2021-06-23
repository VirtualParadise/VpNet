namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.OnObjectClick" />.
    /// </summary>
    public sealed class ObjectClickArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectClickArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar responsible for the click.</param>
        /// <param name="obj">The object which was clicked.</param>
        /// <param name="hitPoint">The point of impact for the click.</param>
        public ObjectClickArgs(Avatar avatar, VpObject obj, Vector3 hitPoint)
        {
            Avatar = avatar;
            Object = obj;
            HitPoint = hitPoint;
        }

        /// <summary>
        ///     Gets the avatar responsible for the click.
        /// </summary>
        /// <value>The avatar responsible for the click.</value>
        public Avatar Avatar { get; }

        /// <summary>
        ///     Gets the point of impact for the click.
        /// </summary>
        /// <value>The point of impact for the click.</value>
        public Vector3 HitPoint { get; }

        /// <summary>
        ///     Gets the object which was clicked. 
        /// </summary>
        /// <value>The object which was clicked.</value>
        public VpObject Object { get; }
    }
}
