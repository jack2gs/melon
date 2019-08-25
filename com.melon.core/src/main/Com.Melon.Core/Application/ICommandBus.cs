using System.Threading;
using System.Threading.Tasks;

namespace Com.Melon.Core.Application
{
    /// <summary>
    /// the command bus
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Send the command synchronizely
        /// </summary>
        /// <param name="command">the command to execute</param>
        void Send(ICommand command);

        /// <summary>
        /// Send the command asynchronizedly
        /// </summary>
        /// <param name="command">the command to execute</param>
        /// <param name="cancellationToken">the token to cancel the command</param>
        /// <returns>the task to execute the command</returns>
        Task AsyncSend(ICommand command, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send the command synchronizedly
        /// </summary>
        /// <typeparam name="TResult">the result of the command</typeparam>
        /// <param name="command">the command to execute</param>
        /// <returns>the result of the command</returns>
        TResult Send<TResult>(ICommand<TResult> command);

        /// <summary>
        /// Send the command asynchronizedly
        /// </summary>
        /// <typeparam name="TResult">the result of the command</typeparam>
        /// <param name="command">the command to execute</param>
        /// <param name="cancellationToken">the token to cancel the command</param>
        /// <returns>the task to execute the command</returns>
        Task<TResult> AsyncSend<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
    }
}
