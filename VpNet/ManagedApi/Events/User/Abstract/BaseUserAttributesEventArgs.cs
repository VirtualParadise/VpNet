using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseUserAttributesEventArgs : TimedEventArgs, IUserAttributesEventArgs
    {
        #region Implementation of IUserAttributesEventArgs<TUserAttributes>

        public IUserAttributes UserAttributes { get; set; }

        #endregion

        protected BaseUserAttributesEventArgs(IUserAttributes userAttributes)
        {
            UserAttributes = userAttributes;
        }

        protected BaseUserAttributesEventArgs()
        {
            
        }
    }
}
