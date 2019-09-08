using Com.Melon.Core.Domain;
using System;

namespace Com.Melon.Core.Unit.Test.Domain
{
    public class ValueObjectContext: IDisposable
    {
        public DummyValueObject ValueObject { get; private set; }

        public DummyIdentifiedValueObject IdentitfiedValueObject { get; private set; }

        public ValueObjectContext()
        {
            ValueObject = new DummyValueObject("FirstName", "Surname");
            IdentitfiedValueObject = new DummyIdentifiedValueObject("FirstName", "Surname");
        }

        public void Dispose()
        {
        }

        public class DummyValueObject : ValueObject<DummyValueObject>
        {
            public string FirstName { get; private set; }

            public string Surname { get; private set; }

            public DummyValueObject(string firstName, string surname)
            {
                FirstName = firstName;
                Surname = surname;
            }
        }

        public class DummyIdentifiedValueObject : IdentifiedValueObject<DummyIdentifiedValueObject>
        {
            public string FirstName { get; private set; }

            public string Surname { get; private set; }

            public DummyIdentifiedValueObject(string firstName, string surname)
            {
                FirstName = firstName;
                Surname = surname;
            }
        }
    }
}
