namespace VpNet.Interfaces
{
    /// <summary>
    /// Avatar Leave event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface IAvatarLeaveEventArgs
    {
        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        IAvatar Avatar { get; set; }
    }
}