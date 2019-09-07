using Com.Melon.Wrap.Site.Core.Domain;
using System.Linq;

namespace Com.Melon.Wrap.Site.Core.Port.Adapter.Persistence
{
    public class SessionRepository : ISessionRepository
    {
        private readonly WrapSiteCoreDbContext _wrapSiteCoreDbContext;

        public SessionRepository(WrapSiteCoreDbContext wrapSiteCoreDbContext)
        {
            _wrapSiteCoreDbContext = wrapSiteCoreDbContext;
        }

        public Session GetSessionByToken(string token)
        {
            return _wrapSiteCoreDbContext.Sessions.SingleOrDefault(x=>x.SessionToken == token);
        }

        public void Save(Session session)
        {
            _wrapSiteCoreDbContext.Sessions.Add(session);
        }

        public void SaveChanges()
        {
            _wrapSiteCoreDbContext.SaveChanges();
        }
    }
}
