namespace Com.Melon.IdentityAccess.Domain
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);

        User GetUserByEmailAndPassword(string email, string password);

        void Save(User user);

        void SaveChanges();
    }
}
