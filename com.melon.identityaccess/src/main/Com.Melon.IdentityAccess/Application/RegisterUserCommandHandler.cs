using System.Threading;
using System.Threading.Tasks;
using Com.Melon.IdentityAccess.Domain;
using MediatR;

namespace Com.Melon.IdentityAccess.Application
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IRegisterUserService _registerUserService;

        public RegisterUserCommandHandler(IRegisterUserService registerUserService)
        {
            _registerUserService = registerUserService;
        }

        public Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _registerUserService.RegisterUser(request.Email, request.Password);

            return Unit.Task;
        }
    }
}
