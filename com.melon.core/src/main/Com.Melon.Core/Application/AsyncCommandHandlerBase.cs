using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Melon.Core.Application
{
    public abstract class AsyncCommandHandlerBase<TCommand> : IAsyncCommandHandler<TCommand>, IRequestHandler<TCommand>
        where TCommand : CommandBase
    {
        public abstract Task Execute(TCommand command, CancellationToken cancellationToken);

        async Task<Unit> IRequestHandler<TCommand, Unit>.Handle(TCommand request, CancellationToken cancellationToken)
        {
            await this.Execute(request, cancellationToken);

            return Unit.Value;
        }
    }

    public abstract class AsyncCommandHandlerBase<TCommand, TResult> : IAsyncCommandHandler<TCommand, TResult>, IRequestHandler<TCommand, TResult>
       where TCommand : CommandBase<TResult>
    {
        public abstract Task<TResult> Execute(TCommand command, CancellationToken cancellationToken);

        async Task<TResult> IRequestHandler<TCommand, TResult>.Handle(TCommand request, CancellationToken cancellationToken)
        {
           return await this.Execute(request, cancellationToken);
        }
    }
}
