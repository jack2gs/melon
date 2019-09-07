using System;

namespace Com.Melon.Wrap.Site.Core.Domain
{
    public class SessionService : ISessionService
    {
        public Session CreateSession(int userId)
        {
            return new Session(userId, Guid.NewGuid().ToString(), DateTime.Now, DateTime.Now.AddMinutes(30));
        }
    }
}
