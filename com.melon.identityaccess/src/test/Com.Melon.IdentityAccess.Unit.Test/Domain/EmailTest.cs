using Com.Melon.IdentityAccess.Domain;
using FluentAssertions;
using System;
using Xunit;
using XunitExtensions;

namespace Com.Melon.IdentityAccess.Unit.Test
{
    public class EmailTestBase: Specification
    {
        protected Exception ActualException;

        protected virtual string GetEmailAddress()
        {
            return null;
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                Email email = new Email(GetEmailAddress());
            });
        }
    }

    public class When_email_is_valid : EmailTestBase
    {
        protected override string GetEmailAddress()
        {
            return "jack.gao@fnz.com";
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_email_is_not_valid : EmailTestBase
    {
        protected override string GetEmailAddress()
        {
            return "jack.gaofnz.com";
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().BeOfType<ArgumentException>();
        }
    }
}
