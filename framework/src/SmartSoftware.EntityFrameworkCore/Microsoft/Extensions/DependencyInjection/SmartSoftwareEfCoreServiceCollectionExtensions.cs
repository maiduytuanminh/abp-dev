using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.DependencyInjection;
using SmartSoftware.MultiTenancy;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareEfCoreServiceCollectionExtensions
{
    public static IServiceCollection AddSmartSoftwareDbContext<TDbContext>(
        this IServiceCollection services,
        Action<ISmartSoftwareDbContextRegistrationOptionsBuilder>? optionsBuilder = null)
        where TDbContext : SmartSoftwareDbContext<TDbContext>
    {
        services.AddMemoryCache();

        var options = new SmartSoftwareDbContextRegistrationOptions(typeof(TDbContext), services);

        var replacedMultiTenantDbContextTypes = typeof(TDbContext).GetCustomAttributes<ReplaceDbContextAttribute>(true)
            .SelectMany(x => x.ReplacedDbContextTypes).ToList();

        foreach (var dbContextType in replacedMultiTenantDbContextTypes)
        {
            options.ReplaceDbContext(dbContextType.Type, multiTenancySides: dbContextType.MultiTenancySide);
        }

        optionsBuilder?.Invoke(options);

        services.TryAddTransient(DbContextOptionsFactory.Create<TDbContext>);

        foreach (var entry in options.ReplacedDbContextTypes)
        {
            var originalDbContextType = entry.Key.Type;
            var targetDbContextType = entry.Value ?? typeof(TDbContext);

            services.Replace(ServiceDescriptor.Transient(originalDbContextType, sp =>
            {
                var dbContextType = sp.GetRequiredService<IEfCoreDbContextTypeProvider>().GetDbContextType(originalDbContextType);
                return sp.GetRequiredService(dbContextType);
            }));

            services.Configure<SmartSoftwareDbContextOptions>(opts =>
            {
                var multiTenantDbContextType = new MultiTenantDbContextType(originalDbContextType, entry.Key.MultiTenancySide);
                opts.DbContextReplacements[multiTenantDbContextType] = targetDbContextType;
            });
        }

        new EfCoreRepositoryRegistrar(options).AddRepositories();

        return services;
    }
}
