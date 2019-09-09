namespace Com.Melon.Core.Domain
{
    public abstract class IdentifiedValueObject<T>: ValueObject<T>, IIdentity
        where T : IdentifiedValueObject<T>
    {
        public int Id { get; protected set; }
    }
}
