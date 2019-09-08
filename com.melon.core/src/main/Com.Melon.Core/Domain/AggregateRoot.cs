namespace Com.Melon.Core.Domain
{
    /// <summary>
    /// Root Entity, Versioned Entity, Concurrency Safe Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AggregateRoot<T>: Entity<T>, IAggregateRoot
        where T: AggregateRoot<T>
    {
        /// <summary>
        /// the timestamp to do the optimistic lock
        /// </summary>
        public byte[] Timestamp { get; private set; }
    }
}
