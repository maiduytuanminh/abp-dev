using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.DependencyInjection;
using SmartSoftware.MongoDB;
using SmartSoftware.MongoDB.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareMongoDbServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDbContext<TMongoDbContext>(this IServiceCollection services, Action<ISmartSoftwareMongoDbContextRegistrationOptionsBuilder>? optionsBuilder = null) //Created overload instead of default parameter
        where TMongoDbContext : SmartSoftwareMongoDbContext
    {
        var options = new SmartSoftwareMongoDbContextRegistrationOptions(typeof(TMongoDbContext), services);

        var replacedDbContextTypes = typeof(TMongoDbContext).GetCustomAttributes<ReplaceDbContextAttribute>(true)
            .SelectMany(x => x.ReplacedDbContextTypes).ToList();

        foreach (var dbContextType in replacedDbContextTypes)
        {
            options.ReplaceDbContext(dbContextType.Type, multiTenancySides: dbContextType.MultiTenancySide);
        }

        optionsBuilder?.Invoke(options);

        foreach (var entry in options.ReplacedDbContextTypes)
        {
            var originalDbContextType = entry.Key.Type;
            var targetDbContextType = entry.Value ?? typeof(TMongoDbContext);

            services.Replace(ServiceDescriptor.Transient(originalDbContextType, sp =>
            {
                var dbContextType = sp.GetRequiredService<IMongoDbContextTypeProvider>().GetDbContextType(originalDbContextType);
                return sp.GetRequiredService(dbContextType);
            }));

            services.Configure<SmartSoftwareMongoDbContextOptions>(opts =>
            {
                var multiTenantDbContextType = new MultiTenantDbContextType(originalDbContextType, entry.Key.MultiTenancySide);
                opts.DbContextReplacements[multiTenantDbContextType] = targetDbContextType;
            });
        }

        new MongoDbRepositoryRegistrar(options).AddRepositories();

        return services;
    }
}
