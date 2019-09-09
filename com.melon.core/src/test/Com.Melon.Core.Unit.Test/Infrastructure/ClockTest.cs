using Com.Melon.Core.Infrastructure;
using FluentAssertions;
using System;
using XunitExtensions;

namespace Com.Melon.Core.Unit.Test.Infrastructure
{
    public class when_fixing_now: Specification
    {
        protected DateTime ExpectedDateTime;

        protected DateTime ActualDateTime;

        protected override void EstablishContext()
        {
            ExpectedDateTime = DateTime.Now;
            Clock.FixNow(ExpectedDateTime);
        }

        protected override void Because()
        {
            ActualDateTime = Clock.Now;
        }

        [Observation]
        void should_get_same_datetime()
        {
            ActualDateTime.Should().Be(ExpectedDateTime);
        }
    }
}
