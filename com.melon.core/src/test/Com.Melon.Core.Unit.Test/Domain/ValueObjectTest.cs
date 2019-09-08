using XunitExtensions;
using Xunit;
using Com.Melon.Core.Domain;
using FluentAssertions;
using System;
using static Com.Melon.Core.Unit.Test.Domain.ValueObjectContext;
using Com.Melon.Core.Infrastructure;

namespace Com.Melon.Core.Unit.Test.Domain
{
    public class ValueObjectTestBase: Specification, IClassFixture<ValueObjectContext>
    {
        public ValueObjectContext Context { get; private set; }

        public DummyValueObject ValueObject { get; private set; }

        public ValueObjectTestBase()
        {
            Context = new ValueObjectContext();
        }

        protected override void EstablishContext()
        {
            ValueObject = Context.ValueObject;
        }
    }

    public class ValueObjectTestEqualityBase : ValueObjectTestBase
    {
        protected bool ActualIsEqual;

        protected DummyValueObject AnotherValueObject { get; set; }

        protected override void Because()
        {
            ActualIsEqual = ValueObject == AnotherValueObject;
        }
    }

    public class When_value_objects_are_same: ValueObjectTestEqualityBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            AnotherValueObject = new DummyValueObject(ValueObject.FirstName, ValueObject.Surname);
        }

        [Observation]
        void should_be_equal()
        {
            ActualIsEqual.Should().BeTrue();
        }
    }

    public class When_value_objects_are_different : ValueObjectTestEqualityBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            AnotherValueObject = new DummyValueObject("FakedFirstName", "FakedSurname");
        }

        [Observation]
        void should_not_be_equal()
        {
            ActualIsEqual.Should().BeFalse();
        }
    }

    public class When_creating_value_object : ValueObjectTestBase
    {
        [Observation]
        void should_implement_IEquatable()
        {
            ValueObject.Should().BeAssignableTo<IEquatable<DummyValueObject>>();
        }

        [Observation]
        void should_implement_IValueObject()
        {
            ValueObject.Should().BeAssignableTo<IValueObject>();
        }

        [Observation]
        void should_be_of_value_object_type()
        {
            ValueObject.Should().BeAssignableTo<ValueObject<DummyValueObject>>();
        }

        [Observation]
        void should_be_of_domain_object_type()
        {
            ValueObject.Should().BeAssignableTo<DomainObject>();
        }

        [Observation]
        void should_be_of_assertion_concern_type()
        {
            ValueObject.Should().BeAssignableTo<AssertionConcern>();
        }
    }
}
