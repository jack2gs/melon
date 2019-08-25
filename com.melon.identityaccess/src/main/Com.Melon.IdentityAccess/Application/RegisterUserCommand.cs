using Com.Melon.Core.Application;

namespace Com.Melon.IdentityAccess.Application
{
    public class RegisterUserCommand: CommandBase
    {
       public string Email { get; set; }

       public string Password { get; set; }
    }
}
