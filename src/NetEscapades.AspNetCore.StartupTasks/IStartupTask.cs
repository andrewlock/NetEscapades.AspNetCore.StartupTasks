using System.Threading;
using System.Threading.Tasks;

namespace NetEscapades.AspNetCore.StartupTasks
{
    /// <summary>
    /// A task to execute before starting the application
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// Execute the startup task, before the WebHost is run
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for cancelling the task</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
