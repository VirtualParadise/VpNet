namespace VpNet.Interfaces
{
    /// <summary>
    /// Templated Avatar functions.
    /// </summary>
    /// <typeparam name="TRc">The type of the result code object.</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface IAvatarFunctions<TAvatar>
        where TAvatar : class, IAvatar,new()
    {
        /// <summary>
        /// Gets or sets the Avatars
        /// </summary>
        /// <value>
        /// </value>
        Dictionary<int, TAvatar> Avatars { get; set; }
        /// <summary>
        /// Announce your bot at a given location.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordiante.</param>
        /// <param name="yaw">Yaw.</param>
        /// <param name="pitch">Pitch.</param>
        /// <returns>Result object</returns>
        void UpdateAvatar(double x = 0.0f, double y = 0.0f, double z = 0.0f, double yaw = 0.0f, double pitch = 0.0f);
        /// <summary>
        /// Announce your bot at a given location with default pitch and yaw.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>Result object</returns>
        void UpdateAvatar(Vector3 position);
        /// <summary>
        /// Announce your bot at a given location.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        void UpdateAvatar(Vector3 position, Vector3 rotation);
        /// <summary>
        /// Send an avatar click event to other users in the world
        /// </summary>
        /// <param name="session">The session id of the clicked avatar.</param>
        /// <returns>Zero when successful, otherwise nonzero.</returns>
        void AvatarClick(int session);
        /// <summary>
        /// Send an avatar click event to other users in the world
        /// </summary>
        /// <param name="avatar">The avatar object containing the session id.</param>
        /// <returns>Zero when successful, otherwise nonzero.</returns>
        void AvatarClick(TAvatar avatar);
        void GetUserProfile(int user);
        void GetUserProfile(TAvatar avatar);
        void GetUserProfile(string userName);
    }
}
