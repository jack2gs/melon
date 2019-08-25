using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Core.Application;
using Com.Melon.IdentityAccess.Domain;
using MediatR;

namespace Com.Melon.IdentityAccess.Application
{
    public class RegisterUserCommandHandler : CommandHandlerBase<RegisterUserCommand>
    {
        private readonly RegisterUserService _registerUserService;

        public RegisterUserCommandHandler(RegisterUserService registerUserService)
        {
            _registerUserService = registerUserService;
        }

        public override void Execute(RegisterUserCommand command)
        {
            _registerUserService.RegisterUser(command.Email, command.Password);
        }
    }
}
