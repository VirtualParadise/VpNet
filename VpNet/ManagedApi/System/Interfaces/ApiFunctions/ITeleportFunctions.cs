namespace VpNet.Interfaces
{
    /// <summary>
    /// Avatar Teleport Functions
    /// </summary>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    public interface ITeleportFunctions<in TWorld, in TAvatar>
        where TAvatar: class, IAvatar, new()
    {
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="world">The world.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns></returns>
        void TeleportAvatar(TAvatar avatar, string world, double x, double y, double z, double yaw, double pitch);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns></returns>
        void TeleportAvatar(TAvatar avatar, string world, Vector3 position, double yaw, double pitch);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="targetSession">The target session.</param>
        /// <param name="world">The world.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns></returns>
        void TeleportAvatar(int targetSession, string world, double x, double y, double z, double yaw, double pitch);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="targetSession">The target session.</param>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns></returns>
        void TeleportAvatar(int targetSession, string world, Vector3 position, double yaw, double pitch);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        void TeleportAvatar(TAvatar avatar, string world, Vector3 position, Vector3 rotation);
        /// <summary>
        /// Teleports the avatar.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        void TeleportAvatar(TAvatar avatar, TWorld world, Vector3 position, Vector3 rotation);
        /// <summary>
        /// Teleports the avatar within the current world.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        void TeleportAvatar(TAvatar avatar, Vector3 position, Vector3 rotation);
        /// <summary>
        /// Teleports the avatar within the current world b previously having changed the avatar position and rotation properties
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <returns></returns>
        void TeleportAvatar(TAvatar avatar);
    }
}
