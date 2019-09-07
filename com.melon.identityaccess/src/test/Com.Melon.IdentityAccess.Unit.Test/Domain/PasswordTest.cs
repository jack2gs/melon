using Com.Melon.IdentityAccess.Domain;
using FluentAssertions;
using System;
using Xunit;
using XunitExtensions;

namespace Com.Melon.IdentityAccess.Unit.Test.Domain
{
    public class PasswordTestBase: Specification
    {
        protected Exception ActualException;

        protected string Password;

        protected override void EstablishContext()
        {
            Password = GetPassword();
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                Password password = new Password(Password);
            });
        }

        protected virtual string GetPassword()
        {
            return null;
        }
    }

    public class When_passowrd_is_valid: PasswordTestBase
    {
        protected override string GetPassword()
        {
            return "gs112233";
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_passowrd_is_invalid : PasswordTestBase
    {
        protected override string GetPassword()
        {
            return "gs11223311111111111111111111111111111111111111111111111";
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
        }
    }
}
