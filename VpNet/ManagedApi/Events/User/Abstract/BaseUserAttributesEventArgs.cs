using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseUserAttributesEventArgs : TimedEventArgs, IUserAttributesEventArgs
    {
        #region Implementation of IUserAttributesEventArgs<TUserAttributes>

        public UserAttributes UserAttributes { get; set; }

        #endregion

        protected BaseUserAttributesEventArgs(UserAttributes userAttributes)
        {
            UserAttributes = userAttributes;
        }

        protected BaseUserAttributesEventArgs()
        {
            
        }
    }
}
