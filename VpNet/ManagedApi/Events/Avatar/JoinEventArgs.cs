using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnJoin" />.
    /// </summary>
    [XmlRoot("OnJoin", Namespace = Global.XmlNsEvent)]
    public sealed class JoinEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JoinEventArgs" /> class.
        /// </summary>
        /// <param name="avatar">The avatar responsible for the join request.</param>
        public JoinEventArgs(Avatar avatar)
        {
            Avatar = avatar;
        }

        /// <summary>
        ///     Gets the avatar responsible for the join request.
        /// </summary>
        /// <value>The avatar responsible for the join request</value>
        public Avatar Avatar { get; }
    }
}