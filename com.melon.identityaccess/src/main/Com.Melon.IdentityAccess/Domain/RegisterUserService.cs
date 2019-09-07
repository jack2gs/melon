using System;

namespace Com.Melon.IdentityAccess.Domain
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) != null)
            {
                throw new ArgumentException("User already exists.");
            }

            User newUser = new User(email, password);

            // TODO: Use event to avoid save it in the domain service
            _userRepository.Save(newUser);
            _userRepository.SaveChanges();
        }
    }
}
