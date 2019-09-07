using Com.Melon.Wrap.Site.Core.Domain;
using MediatR;

namespace Com.Melon.Wrap.Site.Core.Application
{
    public class CreateSessionCommand: IRequest<Session>
    {
        public int UserId { get; private set; }

        public CreateSessionCommand(int userId)
        {
            UserId = userId;
        }
    }
}
