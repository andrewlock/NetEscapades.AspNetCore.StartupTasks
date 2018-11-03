using NetEscapades.AspNetCore.StartupTasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for registering <see cref="IStartupTask"/>s with an <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add an <see cref="IStartupTask"/> registration for the given type.
        /// </summary>
        /// <typeparam name="TStartupTask">An <see cref="IStartupTask"/> to register.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to register with.</param>
        /// <returns>The original <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddStartupTask<TStartupTask>(this IServiceCollection services)
            where TStartupTask : class, IStartupTask
            => services.AddTransient<IStartupTask, TStartupTask>();
    }
}
