namespace VpNet.Interfaces
{
    public interface IJoinEventArgs
    {
        int Id { get; set; }
        string Name { get; set; }
        int UserId { get; set; }
    }
}
