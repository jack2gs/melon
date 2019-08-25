using System.Threading;
using System.Threading.Tasks;

namespace Com.Melon.Core.Application
{
    public interface IAsyncCommandHandler<TCommand>
    {
        Task Execute(TCommand command, CancellationToken cancellationToken);
    }

    public interface IAsyncCommandHandler<TCommand, TResult>
    {
        Task<TResult> Execute(TCommand command, CancellationToken cancellationToken);
    }
}
