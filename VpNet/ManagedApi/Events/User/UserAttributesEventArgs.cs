using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="ManagedApi.Instance.OnUserAttributes" />.
    /// </summary>
    [XmlRoot("OnUserAttributes", Namespace = Global.XmlNsEvent)]
    public sealed class UserAttributesEventArgs : TimedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserAttributesEventArgs" /> class.
        /// </summary>
        /// <param name="userAttributes">The user attributes.</param>
        public UserAttributesEventArgs(UserAttributes userAttributes)
        {
            UserAttributes = userAttributes;
        }

        /// <summary>
        ///     Gets the user attributes.
        /// </summary>
        /// <value>The user attributes.</value>
        public UserAttributes UserAttributes { get; }
    }
}
