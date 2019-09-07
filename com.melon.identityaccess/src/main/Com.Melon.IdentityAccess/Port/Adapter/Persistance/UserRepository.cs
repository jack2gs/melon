using Com.Melon.IdentityAccess.Domain;
using System.Linq;

namespace Com.Melon.IdentityAccess.Port.Adapter.Persistance
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityAccessDbContext _dbContext;

        public UserRepository(IdentityAccessDbContext identityAccessDbContext)
        {
            _dbContext = identityAccessDbContext;
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.SingleOrDefault(x => x.Email.EmailAddress == email);
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return _dbContext.Users.SingleOrDefault(x => x.Email.EmailAddress == email && x.Password.PasswordString == password);
        }

        public void Save(User user)
        {
            _dbContext.Users.Add(user);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
