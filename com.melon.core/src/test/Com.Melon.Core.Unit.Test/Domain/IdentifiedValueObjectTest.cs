using XunitExtensions;
using Xunit;
using Com.Melon.Core.Domain;
using FluentAssertions;
using System;
using static Com.Melon.Core.Unit.Test.Domain.ValueObjectContext;
using Com.Melon.Core.Infrastructure;

namespace Com.Melon.Core.Unit.Test.Domain
{
    public class IdentifiedValueObjectTestBase: Specification, IClassFixture<ValueObjectContext>
    {
        public ValueObjectContext Context { get; private set; }

        public DummyIdentifiedValueObject ValueObject { get; private set; }

        public IdentifiedValueObjectTestBase()
        {
            Context = new ValueObjectContext();
        }

        protected override void EstablishContext()
        {
            ValueObject = Context.IdentitfiedValueObject;
        }
    }

    public class IdentifiedValueObjectTestEqualityBase : IdentifiedValueObjectTestBase
    {
        protected bool ActualIsEqual;

        protected DummyIdentifiedValueObject AnotherValueObject { get; set; }

        protected override void Because()
        {
            ActualIsEqual = ValueObject == AnotherValueObject;
        }
    }

    public class When_identified_value_objects_are_same: IdentifiedValueObjectTestEqualityBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            AnotherValueObject = new DummyIdentifiedValueObject(ValueObject.FirstName, ValueObject.Surname);
        }

        [Observation]
        void should_be_equal()
        {
            ActualIsEqual.Should().BeTrue();
        }
    }

    public class When_identified_value_objects_are_different : IdentifiedValueObjectTestEqualityBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            AnotherValueObject = new DummyIdentifiedValueObject("FakedFirstName", "FakedSurname");
        }

        [Observation]
        void should_not_be_equal()
        {
            ActualIsEqual.Should().BeFalse();
        }
    }

    public class When_creating_identified_value_object : IdentifiedValueObjectTestBase
    {
        [Observation]
        void should_be_assignable_to_ValueObject()
        {
            ValueObject.Should().BeAssignableTo<ValueObject<DummyIdentifiedValueObject>>();
        }

        [Observation]
        void should_be_assignable_to_IIdentity()
        {
            ValueObject.Should().BeAssignableTo<IIdentity>();
        }
    }
}
