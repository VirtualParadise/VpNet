namespace VpNet.Interfaces
{
    /// <summary>
    /// Avatar Change event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface IAvatarChangeEventArgs
    {
        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        IAvatar Avatar { get; set; }
        /// <summary>
        /// Gets or sets the avatar previous state.
        /// </summary>
        /// <value>
        /// The avatar previous.
        /// </value>
        IAvatar AvatarPrevious { get; set; }
        /// <summary>
        /// Gets or sets the time span of the change
        /// </summary>
        /// <value>
        /// The time span.
        /// </value>
        System.TimeSpan TimeSpan { get; }
    }
}