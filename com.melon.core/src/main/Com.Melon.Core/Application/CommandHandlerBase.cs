using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Melon.Core.Application
{
    public abstract class CommandHandlerBase<TCommand> : IRequestHandler<TCommand>, ICommandHandler<TCommand>
        where TCommand : CommandBase
    {
        Task<Unit> IRequestHandler<TCommand, Unit>.Handle(TCommand request, CancellationToken cancellationToken)
        {
            Execute(request);
            return Unit.Task;
        }

        public abstract void Execute(TCommand command);
    }

    public abstract class CommandHandlerBase<TCommand, TResult> : IRequestHandler<TCommand, TResult>, ICommandHandler<TCommand, TResult>
       where TCommand : CommandBase<TResult>
    {
        Task<TResult> IRequestHandler<TCommand, TResult>.Handle(TCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(request));
        }

        public abstract TResult Execute(TCommand command);
    }
}
