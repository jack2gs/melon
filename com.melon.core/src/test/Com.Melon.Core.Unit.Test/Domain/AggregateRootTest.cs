using System;
using Com.Melon.Core.Domain;
using Com.Melon.Core.Infrastructure;
using FluentAssertions;
using Xunit;
using XunitExtensions;
using static Com.Melon.Core.Unit.Test.Domain.EntityTestContext;

namespace Com.Melon.Core.Unit.Test.Domain
{
    public class AggregateRootTest: Specification, IClassFixture<EntityTestContext>
    {
        protected DummyAggregateRoot AggregateRoot;

        protected DateTime DateTimeNow;

        protected override void EstablishContext()
        {
            DateTimeNow = DateTime.Now;
            Clock.FixNow(DateTimeNow);
            AggregateRoot = new DummyAggregateRoot();
        }

        protected override void DestroyContext()
        {
            Clock.Resume();
        }

        [Observation]
        void should_be_assignable_to_Entity()
        {
            AggregateRoot.Should().BeAssignableTo<Entity<DummyAggregateRoot>>();
        }

        [Observation]
        void should_be_assignable_to_IAggregateRoot()
        {
            AggregateRoot.Should().BeAssignableTo<IAggregateRoot>();
        }

        [Observation]
        void should_have_datetime_stamp()
        {
            AggregateRoot.DateTimeCreated.Should().Be(DateTimeNow);
            AggregateRoot.DateTimeLastModified.Should().Be(DateTimeNow);
        }
    }
}
