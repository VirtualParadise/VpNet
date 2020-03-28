namespace VpNet.Interfaces
{
    /// <summary>
    /// Avatar Leave event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface IAvatarLeaveEventArgs<TAvatar> 
        where TAvatar : class, IAvatar, new()
    {
        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        TAvatar Avatar { get; set; }
    }
}