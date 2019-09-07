namespace Com.Melon.IdentityAccess.Domain
{
    public interface IRegisterUserService
    {
        void RegisterUser(string email, string password);
    }
}
