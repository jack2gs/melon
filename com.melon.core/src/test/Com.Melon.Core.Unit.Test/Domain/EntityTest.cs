using Com.Melon.Core.Domain;
using FluentAssertions;
using System;
using Xunit;
using XunitExtensions;
using static Com.Melon.Core.Unit.Test.Domain.EntityTestContext;

namespace Com.Melon.Core.Unit.Test.Domain
{
    public class EntityTestBase: Specification, IClassFixture<EntityTestContext>
    {
        protected DummyEntity Entity;

        protected EntityTestContext Context;

        protected override void EstablishContext()
        {
            Context = new EntityTestContext();
        }

        protected override void Because()
        {
            Entity = Context.Entity; 
        }
    }

    public class EntityEqualityTestBase: EntityTestBase
    {
        protected DummyEntity AnotherEntity;

        protected bool ActualIsEqual;

        protected override void EstablishContext()
        {
            Context = new EntityTestContext();
            Entity = Context.Entity;
            AnotherEntity = new DummyEntity(Entity.Id, "FakedFirstName", "FakedSurname");
        }

        protected override void Because()
        {
            ActualIsEqual = Entity == AnotherEntity; 
        }
    }

    public class When_two_entities_are_the_same: EntityEqualityTestBase
    {
        [Observation]
        void should_be_same()
        {
            ActualIsEqual.Should().BeTrue();
        }
    }

    public class When_two_entities_are_different : EntityEqualityTestBase
    {
        protected override void EstablishContext()
        {
            Context = new EntityTestContext();
            Entity = Context.Entity;
            AnotherEntity = new DummyEntity(Entity.Id + 1, "FirstName", "Surname");
        }

        [Observation]
        void should_be_same()
        {
            ActualIsEqual.Should().BeFalse();
        }
    }

    public class When_after_creating_entity: EntityTestBase
    {
        [Observation]
        void should_be_assignable_to_DomainObject()
        {
            Entity.Should().BeAssignableTo<DomainObject>();
        }

        [Observation]
        void should_be_assignable_to_IEquatable()
        {
            Entity.Should().BeAssignableTo<IEquatable<DummyEntity>>();
        }

        [Observation]
        void should_be_assignable_to_IEntity()
        {
            Entity.Should().BeAssignableTo<IEntity>();
        }
    }
}
