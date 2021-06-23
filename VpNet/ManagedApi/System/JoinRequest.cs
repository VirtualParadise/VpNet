using System.Threading.Tasks;
using VpNet.NativeApi;

namespace VpNet
{
    /// <summary>
    ///     Represents a join request.
    /// </summary>
    public class JoinRequest
    {
        private readonly VirtualParadiseClient _virtualParadiseClient;
        private readonly int _requestId;

        internal JoinRequest(VirtualParadiseClient virtualParadiseClient, int requestId, int userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            _virtualParadiseClient = virtualParadiseClient;
            _requestId = requestId;
        }
        
        /// <summary>
        ///     Gets the ID of the user which sent the request.
        /// </summary>
        /// <value>The ID of the user which sent the request.</value>
        public int UserId { get; }
        
        /// <summary>
        ///     Gets the name of the user which sent the request.
        /// </summary>
        /// <value>The name of the user which sent the request.</value>
        public string UserName { get; }
        
        /// <summary>
        ///     Accepts the join request and signals the current location of the instance to the requester.
        /// </summary>
        public Task AcceptAsync()
        {
            lock (_virtualParadiseClient)
            {
                World world = _virtualParadiseClient.World;
                Vector3 position = _virtualParadiseClient.My().Position;
                Vector3 rotation = _virtualParadiseClient.My().Rotation;

                return AcceptAsync(new Location(world, position, rotation));
            }
        }

        /// <summary>
        ///     Accepts the join request and signals a target position, within the current world of the instance, to the requester.
        /// </summary>
        /// <param name="position">The target position, in the current world, of the join.</param>
        public Task AcceptAsync(Vector3 position)
        {
            lock (_virtualParadiseClient)
            {
                return AcceptAsync(new Location(_virtualParadiseClient.World, position, Vector3.Zero));
            }
        }

        /// <summary>
        ///     Accepts the join request and signals a target location to the requester.
        /// </summary>
        /// <param name="location">The target location of the join.</param>
        public Task AcceptAsync(Location location)
        {
            lock (_virtualParadiseClient)
            {
                World world = location.World;
                Vector3 position = location.Position;
                Vector3 rotation = location.Rotation;
                
                int rc = Functions.vp_join_accept(_virtualParadiseClient.InternalInstance, _requestId,
                    world.Name,
                    position.X, position.Y, position.Z, 
                    (float) rotation.Y, (float) rotation.X);
                VirtualParadiseClient.CheckReasonCode(rc);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Declines the join request.
        /// </summary>
        public Task DeclineAsync()
        {
            lock (_virtualParadiseClient)
            {
                int rc = Functions.vp_join_decline(_virtualParadiseClient.InternalInstance, _requestId);
                VirtualParadiseClient.CheckReasonCode(rc);
            }
            return Task.CompletedTask;
        }
    }
}
