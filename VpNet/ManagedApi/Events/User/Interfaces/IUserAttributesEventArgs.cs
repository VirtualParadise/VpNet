namespace VpNet.Interfaces
{
    /// <summary>
    /// User Attribute event arguments non templated interface specifications.
    /// </summary>
    public interface IUserAttributesEventArgs
    {
        UserAttributes UserAttributes { get; set; }
    }
}