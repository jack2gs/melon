namespace Com.Melon.Core.Domain
{
    public interface IAggregateRoot
    {
        byte[] Timestamp { get; }
    }
}
