using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NetEscapades.AspNetCore.StartupTasks;

namespace Microsoft.AspNetCore.Hosting
{
    /// <summary>
    /// Extensions for running tasks and starting the <see cref="IWebHost"/>
    /// </summary>
    public static class StartupTaskWebHostExtensions
    {
        /// <summary>
        /// Runs all registered <see cref="IStartupTask"/>s and then calls <see cref="Microsoft.AspNetCore.Hosting.WebHostExtensions.RunAsync(IWebHost, CancellationToken)"/>
        /// </summary>
        /// <param name="webHost">The <see cref="IWebHost"/> to load <see cref="IStartupTask"/>s from</param>
        /// <param name="cancellationToken">A cancellation token for cancelling tasks and startup</param>
        /// <returns>The original <see cref="IWebHost"/>.</returns>
        public static async Task RunWithTasksAsync(this IWebHost webHost, CancellationToken cancellationToken = default)
        {
            var startupTasks = webHost.Services.GetServices<IStartupTask>();

            foreach (var startupTask in startupTasks)
            {
                await startupTask.ExecuteAsync(cancellationToken);
            }

            await webHost.RunAsync(cancellationToken);
        }
    }
}
