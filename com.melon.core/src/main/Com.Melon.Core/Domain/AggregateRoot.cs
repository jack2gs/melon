using System;
using Com.Melon.Core.Infrastructure;

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
        
        public DateTime DateTimeCreated { get; protected set; }
        
        public DateTime DateTimeLastModified { get; protected set; }

        protected AggregateRoot(int id, DateTime dateTimeCreated, DateTime dateTimeLastModified) :base(id)
        {
            DateTimeCreated = dateTimeCreated;
            DateTimeLastModified = dateTimeLastModified;
        }

        protected AggregateRoot():this(0,Clock.Now,Clock.Now)
        {
        }
    }
}
