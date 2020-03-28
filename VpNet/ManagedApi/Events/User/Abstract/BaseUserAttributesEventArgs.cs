using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract class BaseUserAttributesEventArgs<TUserAttributes> :
        TimedEventArgs, IUserAttributesEventArgs<TUserAttributes>
        where TUserAttributes : class, IUserAttributes, new()
    {
        #region Implementation of IUserAttributesEventArgs<TUserAttributes>

        public TUserAttributes UserAttributes { get; set; }

        #endregion

        protected BaseUserAttributesEventArgs(TUserAttributes userAttributes)
        {
            UserAttributes = userAttributes;
        }

        protected BaseUserAttributesEventArgs()
        {
            
        }
    }
}
