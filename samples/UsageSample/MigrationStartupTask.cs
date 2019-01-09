using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetEscapades.AspNetCore.StartupTasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsageSample.Data;

namespace UsageSample
{
    public class MigrationStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await dbContext.Database.MigrateAsync();
            }
        }

        public Task ShutdownAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
