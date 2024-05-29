using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.MemoryDb;
using SmartSoftware.MemoryDb.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareMemoryDbServiceCollectionExtensions
{
    public static IServiceCollection AddMemoryDbContext<TMemoryDbContext>(this IServiceCollection services, Action<ISmartSoftwareMemoryDbContextRegistrationOptionsBuilder>? optionsBuilder = null)
        where TMemoryDbContext : MemoryDbContext
    {
        var options = new SmartSoftwareMemoryDbContextRegistrationOptions(typeof(TMemoryDbContext), services);
        optionsBuilder?.Invoke(options);

        if (options.DefaultRepositoryDbContextType != typeof(TMemoryDbContext))
        {
            services.TryAddSingleton(options.DefaultRepositoryDbContextType, sp => sp.GetRequiredService<TMemoryDbContext>());
        }

        foreach (var entry in options.ReplacedDbContextTypes)
        {
            var originalDbContextType = entry.Key.Type;
            var targetDbContextType = entry.Value ?? typeof(TMemoryDbContext);

            services.Replace(
                ServiceDescriptor.Singleton(
                    originalDbContextType,
                    sp => sp.GetRequiredService(targetDbContextType)
                )
            );
        }

        new MemoryDbRepositoryRegistrar(options).AddRepositories();

        return services;
    }
}
