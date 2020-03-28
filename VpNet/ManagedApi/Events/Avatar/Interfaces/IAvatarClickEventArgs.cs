namespace VpNet.Interfaces
{
    /// <summary>
    /// Avater enter event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface IAvatarClickEventArgs<TAvatar>
        where TAvatar : class, IAvatar, new()
    {
        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        TAvatar Avatar { get; set; }
        /// <summary>
        /// Gets or sets the clicked avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        TAvatar ClickedAvatar { get; set; }
        /// <summary>
        /// Gets or sets the world hit coordinates
        /// </summary>
        /// <value>
        /// The world hit coordinates
        /// </value>
        Vector3 WorldHit { get; set; }
    }
}