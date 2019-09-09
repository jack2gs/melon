using Com.Melon.Core.Domain;

namespace Com.Melon.Core.Unit.Test.Domain
{
    public class EntityTestContext
    {
        public DummyEntity Entity { get; private set; }

        public DummyAggregateRoot AggregateRoot { get; private set; }

        public EntityTestContext()
        {
            Entity = new DummyEntity(1, "FirstName", "Surname");
            AggregateRoot = new DummyAggregateRoot();
        }

        public class DummyEntity: Entity<DummyEntity>
        {
            public string FirstName { get; private set; }

            public string Surname { get; private set; }

            public DummyEntity(int id, string firstName, string surname)
            {
                Id = id;
                FirstName = firstName;
                Surname = surname;
            }
        }

        public class DummyAggregateRoot: AggregateRoot<DummyAggregateRoot>
        {

        }
    }
}
