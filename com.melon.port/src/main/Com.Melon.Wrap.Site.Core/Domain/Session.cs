using Com.Melon.Core.Domain;
using System;

namespace Com.Melon.Wrap.Site.Core.Domain
{
    public class Session: AggregateRoot<Session>
    {
        public string SessionToken { get; private set; }

        public int UserId { get; private set; }

        public DateTime CreatedDateTime { get; private set; }

        public DateTime ExpiredDateTime { get; private set; }

        public Session(int userId, string sessionToken, DateTime createdDateTime, DateTime expiredDateTime)
        {
            UserId = userId;
            SessionToken = sessionToken;
            CreatedDateTime = createdDateTime;
            ExpiredDateTime = expiredDateTime;
        }
    }
}
