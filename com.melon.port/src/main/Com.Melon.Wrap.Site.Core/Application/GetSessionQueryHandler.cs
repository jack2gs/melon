using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Wrap.Site.Core.Domain;
using MediatR;

namespace Com.Melon.Wrap.Site.Core.Application
{
    public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, Session>
    {
        private readonly ISessionRepository _sessionRepository;

        public GetSessionQueryHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public Task<Session> Handle(GetSessionQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_sessionRepository.GetSessionByToken(request.SessionToken));
        }
    }
}
