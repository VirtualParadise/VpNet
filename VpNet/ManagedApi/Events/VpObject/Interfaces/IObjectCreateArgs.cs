namespace VpNet.Interfaces
{
    /// <summary>
    /// Object Create event arguments templated interface specifications.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVpObject">The type of the vp object.</typeparam>
    public interface IObjectCreateArgs<TAvatar, TVpObject>
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    {
        /// <summary>
        /// Gets or sets the vp object.
        /// </summary>
        /// <value>
        /// The vp object.
        /// </value>
        TVpObject VpObject { get; set; }
        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        TAvatar Avatar { get; set; }
    }
}