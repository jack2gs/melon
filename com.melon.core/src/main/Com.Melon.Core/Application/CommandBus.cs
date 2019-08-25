using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Melon.Core.Application
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public CommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task AsyncSend(ICommand command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }

        public Task<TResult> AsyncSend<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }

        public void Send(ICommand command)
        {
            // TODO: here mediatr is used.
            // Later this should be refactored, as it should not use the task 
            var task = _mediator.Send(command);
            task.GetAwaiter().GetResult();
        }

        public TResult Send<TResult>(ICommand<TResult> command)
        {
            // TODO: here mediatr is used.
            // Later this should be refactored, as it should not use the task 
            var task = _mediator.Send(command);
            return task.GetAwaiter().GetResult();
        }
    }
}
