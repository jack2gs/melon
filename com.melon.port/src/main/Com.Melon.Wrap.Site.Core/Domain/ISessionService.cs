namespace Com.Melon.Wrap.Site.Core.Domain
{
    public interface ISessionService
    {
        Session CreateSession(int userId);
    }
}
