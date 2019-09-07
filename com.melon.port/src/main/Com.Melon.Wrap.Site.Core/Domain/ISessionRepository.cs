namespace Com.Melon.Wrap.Site.Core.Domain
{
    public interface ISessionRepository
    {
        void Save(Session session);

        Session GetSessionByToken(string token);

        void SaveChanges();
    }
}
