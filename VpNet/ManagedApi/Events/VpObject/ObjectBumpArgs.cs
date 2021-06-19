using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="ManagedApi.Instance.OnObjectBump" />.
    /// </summary>
    [XmlRoot("OnObjectBump", Namespace = Global.XmlNsEvent)]
    public sealed class ObjectBumpArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectBumpArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar responsible for the bump.</param>
        /// <param name="obj">The object involved in the bump.</param>
        /// <param name="bumpType">The bump type.</param>
        public ObjectBumpArgs(Avatar avatar, VpObject obj, BumpType bumpType)
        {
            Avatar = avatar;
            BumpType = bumpType;
            Object = obj;
        }

        /// <summary>
        ///     Gets the avatar responsible for the bump.
        /// </summary>
        /// <value>The avatar responsible for the bump.</value>
        public Avatar Avatar { get; }
        
        /// <summary>
        ///     Gets the bump type.
        /// </summary>
        /// <value>The bump type.</value>
        public BumpType BumpType { get; }
        
        /// <summary>
        ///     Gets the object involved in the bump. 
        /// </summary>
        /// <value>The object involved in the bump.</value>
        public VpObject Object { get; }
    }
}