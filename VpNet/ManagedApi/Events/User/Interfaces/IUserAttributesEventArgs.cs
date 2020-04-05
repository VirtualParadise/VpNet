namespace VpNet.Interfaces
{
    /// <summary>
    /// User Attribute event arguments non templated interface specifications.
    /// </summary>
    public interface IUserAttributesEventArgs
    {
        IUserAttributes UserAttributes { get; set; }
    }
}