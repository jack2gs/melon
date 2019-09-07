using Com.Melon.IdentityAccess.Application;
using Com.Melon.IdentityAccess.Domain;
using Moq;
using XunitExtensions;

namespace Com.Melon.IdentityAccess.Unit.Test.Application
{
    public class When_register_user: Specification
    {
        protected RegisterUserCommandHandler UnderTest;

        protected Mock<IRegisterUserService> RegisterUserServiceMock;

        protected RegisterUserCommand ExpectedCommand; 

        protected override void EstablishContext()
        {
            RegisterUserServiceMock = new Mock<IRegisterUserService>();
            ExpectedCommand = new RegisterUserCommand("jack@test.com","gs112233");
            UnderTest = new RegisterUserCommandHandler(RegisterUserServiceMock.Object);
        }

        protected override void Because()
        {
            UnderTest.Handle(ExpectedCommand, new System.Threading.CancellationToken());
        }

        [Observation]
        public void should_call_register_service()
        {
            RegisterUserServiceMock.Verify(x => x.RegisterUser(ExpectedCommand.Email, ExpectedCommand.Password), Times.Once);
        }
    }
}
