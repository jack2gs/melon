using System;

namespace Com.Melon.IdentityAccess.Domain
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IUserRepository _userRepository;

        public void RegisterUser(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) != null)
            {
                throw new ArgumentException("User already exists.");
            }

            User newUser = new User(password, password);

            _userRepository.Save(newUser);
        }
    }
}
