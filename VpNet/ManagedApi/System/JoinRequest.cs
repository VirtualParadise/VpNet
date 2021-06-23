using System.Threading.Tasks;
using VpNet.NativeApi;

namespace VpNet
{
    /// <summary>
    ///     Represents a join request.
    /// </summary>
    public class JoinRequest
    {
        private readonly Instance _instance;
        private readonly int _requestId;

        internal JoinRequest(Instance instance, int requestId, int userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            _instance = instance;
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
            lock (_instance)
            {
                World world = _instance.World;
                Vector3 position = _instance.My().Position;
                Vector3 rotation = _instance.My().Rotation;

                return AcceptAsync(new Location(world, position, rotation));
            }
        }

        /// <summary>
        ///     Accepts the join request and signals a target position, within the current world of the instance, to the requester.
        /// </summary>
        /// <param name="position">The target position, in the current world, of the join.</param>
        public Task AcceptAsync(Vector3 position)
        {
            lock (_instance)
            {
                return AcceptAsync(new Location(_instance.World, position, Vector3.Zero));
            }
        }

        /// <summary>
        ///     Accepts the join request and signals a target location to the requester.
        /// </summary>
        /// <param name="location">The target location of the join.</param>
        public Task AcceptAsync(Location location)
        {
            lock (_instance)
            {
                World world = location.World;
                Vector3 position = location.Position;
                Vector3 rotation = location.Rotation;
                
                int rc = Functions.vp_join_accept(_instance.InternalInstance, _requestId,
                    world.Name,
                    position.X, position.Y, position.Z, 
                    (float) rotation.Y, (float) rotation.X);
                Instance.CheckReasonCode(rc);
            }
            return Task.CompletedTask;
        }

        public Task DeclineAsync()
        {
            lock (_instance)
            {
                int rc = Functions.vp_join_decline(_instance.InternalInstance, _requestId);
                Instance.CheckReasonCode(rc);
            }
            return Task.CompletedTask;
        }
    }
}
