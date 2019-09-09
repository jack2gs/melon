using Com.Melon.Core.Domain;
using FluentAssertions;
using Xunit;
using XunitExtensions;
using static Com.Melon.Core.Unit.Test.Domain.EntityTestContext;

namespace Com.Melon.Core.Unit.Test.Domain
{
    public class AggregateRootTest: Specification, IClassFixture<EntityTestContext>
    {
        protected DummyAggregateRoot AggregateRoot;

        protected override void EstablishContext()
        {
            AggregateRoot = new DummyAggregateRoot();
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
    }
}
