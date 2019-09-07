using Com.Melon.Wrap.Site.Core.Domain;
using MediatR;

namespace Com.Melon.Wrap.Site.Core.Application
{
    public class GetSessionQuery: IRequest<Session>
    {
        public string SessionToken { get; private set; }

        public GetSessionQuery(string sessionToken)
        {
            SessionToken = sessionToken;
        }
    }
}
