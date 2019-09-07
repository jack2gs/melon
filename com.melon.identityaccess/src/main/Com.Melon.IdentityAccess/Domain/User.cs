using Com.Melon.Core.Domain;

namespace Com.Melon.IdentityAccess.Domain
{
    /// <summary>
    /// the user entity, it's the aggregation root
    /// </summary>
    public class User: AggregateRoot<User>
    {
        public Email Email { get; private set; }

        public Password Password { get; private set; }

        public User(string email, string password)
        {
            Email = new Email(email);

            Password = new Password(password);
        }

        private User() { }
    }
}
