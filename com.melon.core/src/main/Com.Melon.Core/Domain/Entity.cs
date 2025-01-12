﻿using Com.Melon.Core.Infrastructure;
using System;

namespace Com.Melon.Core.Domain
{
    public abstract class Entity<T>: DomainObject, IEquatable<T>, IEntity
        where T : Entity<T>
    {
        protected Entity(int id)
        {
            Id = id;
        }
        
        protected Entity():this(0)
        {
        }
        
        public int Id { get; protected set; }

        public bool Equals(T other)
        {
            // If the other is null, it can't be equal
            if (ReferenceEquals(other, null)) return false;

            // If they're the same object, then they're equal
            if (ReferenceEquals(this, other)) return true;

            // If they're not the same type, they can't be equal
            if (this.GetType() != other.GetType()) return false;

            // to make sure id are equal 
            return this.Id == other.Id;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            return this.Equals(obj as T);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<T> object1, Entity<T> object2)
        {
            if(object.ReferenceEquals(object1, null))
            {
                return object.ReferenceEquals(object2, null);
            }

            return object1.Equals(object2);
        }

        public static bool operator !=(Entity<T> object1, Entity<T> object2)
        {
            return !(object1 == object2);
        }
    }
}
