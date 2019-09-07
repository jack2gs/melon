using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Wrap.Site.Core.Domain;
using MediatR;

namespace Com.Melon.Wrap.Site.Core.Application
{
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Session>
    {
        private readonly ISessionService _sessionService;

        private readonly ISessionRepository _sessionRepository;

        public CreateSessionCommandHandler(ISessionService sessionService, ISessionRepository sessionRepository)
        {
            _sessionService = sessionService;
            _sessionRepository = sessionRepository;
        }

        public Task<Session> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            Session session = _sessionService.CreateSession(request.UserId);

            _sessionRepository.Save(session);
            _sessionRepository.SaveChanges();

            return Task.FromResult(session);
        }
    }
}
