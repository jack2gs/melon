using System.Threading;
using System.Threading.Tasks;
using Com.Melon.IdentityAccess.Domain;
using MediatR;

namespace Com.Melon.IdentityAccess.Application
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userRepository.GetUserByEmailAndPassword(request.Email, request.Password));
        }
    }
}
