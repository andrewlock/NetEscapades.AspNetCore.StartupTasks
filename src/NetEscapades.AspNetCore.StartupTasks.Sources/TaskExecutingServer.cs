using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;

namespace NetEscapades.AspNetCore.StartupTasks
{
    /// <summary>
    /// A server that executes <see cref="IStartupTask"/>s on startup,
    /// and then invokes a wrapped <see cref="IServer"/> instance
    /// </summary>
    public class TaskExecutingServer : IServer
    {
        private readonly IServer _server;
        private readonly IEnumerable<IStartupTask> _startupTasks;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskExecutingServer"/> class.
        /// </summary>
        /// <param name="server">The decorated server instance </param>
        /// <param name="startupTasks">The tasks to execute on startup</param>
        public TaskExecutingServer(IServer server, IEnumerable<IStartupTask> startupTasks)
        {
            _server = server;
            _startupTasks = startupTasks;
        }

        /// <inheritdoc />
        public IFeatureCollection Features => _server.Features;

        /// <inheritdoc />
        public async Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            foreach (var startupTask in _startupTasks)
            {
                await startupTask.StartAsync(cancellationToken);
            }

            await _server.StartAsync(application, cancellationToken);
        }

        /// <inheritdoc />
        public void Dispose() => _server.Dispose();

        /// <inheritdoc />
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _server.StopAsync(cancellationToken);

            foreach (var startupTask in _startupTasks)
            {
                await startupTask.ShutdownAsync(cancellationToken);
            }
        }
    }
}
