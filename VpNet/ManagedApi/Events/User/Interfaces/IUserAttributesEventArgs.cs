namespace VpNet.Interfaces
{
    /// <summary>
    /// User Attribute event arguments non templated interface specifications.
    /// </summary>
    public interface IUserAttributesEventArgs<TUserAttributes>
        where TUserAttributes : class, IUserAttributes, new()
    {
        TUserAttributes UserAttributes { get; set; }
    }
}