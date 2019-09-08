using System;
using System.Reflection;

namespace Com.Melon.Core.Domain
{
    public abstract class ValueObject<T> : DomainObject, IEquatable<T>, IValueObject
    where T : ValueObject<T>
    {
        public bool Equals(T other)
        {
            // If the other is null, it can't be equal
            if (ReferenceEquals(other, null)) return false;

            // If they're the same object, then they're equal
            if (ReferenceEquals(this, other)) return true;

            // If they're not the same type, they can't be equal
            if (this.GetType() != other.GetType()) return false;

            // to make sure all the fields are equal
            foreach (var fieldInfo in typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (fieldInfo.GetValue(this) != fieldInfo.GetValue(other))
                {
                    return false;
                }
            }

            return true;
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
            var fieldInfos = typeof(T).GetFields();

            if (fieldInfos.Length == 0)
            {
                return base.GetHashCode();
            }

            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;

                foreach (var fieldInfo in fieldInfos)
                {
                    hash = hash * 23 + fieldInfo.GetValue(this).GetHashCode();
                }

                return hash;
            }
        }

        public static bool operator ==(ValueObject<T> object1, ValueObject<T> object2)
        {
            if (object.ReferenceEquals(object1, null))
            {
                return object.ReferenceEquals(object2, null);
            }

            return object1.Equals(object2);
        }

        public static bool operator !=(ValueObject<T> object1, ValueObject<T> object2)
        {
            return !(object1 == object2);
        }
    }
}
