namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="VirtualParadiseClient.AvatarClicked" />.
    /// </summary>
    public sealed class AvatarClickEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AvatarClickEventArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar responsible for the click.</param>
        /// <param name="clickedAvatar">The avatar which was clicked.</param>
        /// <param name="hitPoint">The point of impact of the click.</param>
        public AvatarClickEventArgs(Avatar avatar, Avatar clickedAvatar, Vector3 hitPoint)
        {
            Avatar = avatar;
            ClickedAvatar = clickedAvatar;
            HitPoint = hitPoint;
        }

        /// <summary>
        ///     Gets the avatar responsible for the click.
        /// </summary>
        /// <value>The avatar responsible for the click.</value>
        public Avatar Avatar { get; }
        
        /// <summary>
        ///     Gets the avatar which was clicked.
        /// </summary>
        /// <value>The avatar which was clicked.</value>
        public Avatar ClickedAvatar { get; }
        
        /// <summary>
        ///     Gets the point of impact of the click.
        /// </summary>
        /// <value>The point of impact.</value>
        public Vector3 HitPoint { get; }
    }
}
