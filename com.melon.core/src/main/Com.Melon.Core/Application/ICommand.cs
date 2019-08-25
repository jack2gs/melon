using MediatR;

namespace Com.Melon.Core.Application
{
    /// <summary>
    /// the command interface without return value for the application layer
    /// </summary>
    public interface ICommand: IRequest
    {
    }

    /// <summary>
    /// the command interface with returen value for the application layer
    /// </summary>
    /// <typeparam name="TResult">the return type of the command</typeparam>
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
