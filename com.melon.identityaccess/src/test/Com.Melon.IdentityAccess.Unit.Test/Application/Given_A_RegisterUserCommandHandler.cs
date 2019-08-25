using AutoFixture;
using AutoFixture.AutoMoq;
using Com.Melon.IdentityAccess.Application;
using Com.Melon.IdentityAccess.Domain;
using Moq;
using Xunit;
using MediatR;
using System.Threading;

namespace Com.Melon.IdentityAccess.Unit.Test.Application
{
    public class Given_A_RegisterUserCommandHandler
    {
        protected RegisterUserCommandHandler UnderTest;

        public Given_A_RegisterUserCommandHandler()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Freeze<Mock<IRegisterUserService>>();
            UnderTest = fixture.Create<RegisterUserCommandHandler>();
        }

        public class When_register_a_user: Given_A_RegisterUserCommandHandler
        {
            [Fact]
            public void Then_register_user_service_should_be_called()
            {
                UnderTest.Execute(new RegisterUserCommand());
            }
        }
    }
}
