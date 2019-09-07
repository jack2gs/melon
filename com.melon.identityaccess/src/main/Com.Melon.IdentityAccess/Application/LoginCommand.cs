using Com.Melon.IdentityAccess.Domain;
using MediatR;

namespace Com.Melon.IdentityAccess.Application
{
    public class LoginCommand: IRequest<User>
    {
        public string Email { get; private set; }

        public string Password { get; private set; }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
