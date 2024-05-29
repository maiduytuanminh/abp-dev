using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Data;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.EntityFrameworkCore.DependencyInjection;

public static class DbContextOptionsFactory
{
    public static DbContextOptions<TDbContext> Create<TDbContext>(IServiceProvider serviceProvider)
        where TDbContext : SmartSoftwareDbContext<TDbContext>
    {
        var creationContext = GetCreationContext<TDbContext>(serviceProvider);

        var context = new SmartSoftwareDbContextConfigurationContext<TDbContext>(
            creationContext.ConnectionString,
            serviceProvider,
            creationContext.ConnectionStringName,
            creationContext.ExistingConnection
        );

        var options = GetDbContextOptions<TDbContext>(serviceProvider);

        PreConfigure(options, context);
        Configure(options, context);

        return context.DbContextOptions.Options;
    }

    private static void PreConfigure<TDbContext>(
        SmartSoftwareDbContextOptions options,
        SmartSoftwareDbContextConfigurationContext<TDbContext> context)
        where TDbContext : SmartSoftwareDbContext<TDbContext>
    {
        foreach (var defaultPreConfigureAction in options.DefaultPreConfigureActions)
        {
            defaultPreConfigureAction.Invoke(context);
        }

        var preConfigureActions = options.PreConfigureActions.GetOrDefault(typeof(TDbContext));
        if (!preConfigureActions.IsNullOrEmpty())
        {
            foreach (var preConfigureAction in preConfigureActions!)
            {
                ((Action<SmartSoftwareDbContextConfigurationContext<TDbContext>>)preConfigureAction).Invoke(context);
            }
        }
    }

    private static void Configure<TDbContext>(
        SmartSoftwareDbContextOptions options,
        SmartSoftwareDbContextConfigurationContext<TDbContext> context)
        where TDbContext : SmartSoftwareDbContext<TDbContext>
    {
        var configureAction = options.ConfigureActions.GetOrDefault(typeof(TDbContext));
        if (configureAction != null)
        {
            ((Action<SmartSoftwareDbContextConfigurationContext<TDbContext>>)configureAction).Invoke(context);
        }
        else if (options.DefaultConfigureAction != null)
        {
            options.DefaultConfigureAction.Invoke(context);
        }
        else
        {
            throw new SmartSoftwareException(
                $"No configuration found for {typeof(DbContext).AssemblyQualifiedName}! Use services.Configure<SmartSoftwareDbContextOptions>(...) to configure it.");
        }
    }

    private static SmartSoftwareDbContextOptions GetDbContextOptions<TDbContext>(IServiceProvider serviceProvider)
        where TDbContext : SmartSoftwareDbContext<TDbContext>
    {
        return serviceProvider.GetRequiredService<IOptions<SmartSoftwareDbContextOptions>>().Value;
    }

    private static DbContextCreationContext GetCreationContext<TDbContext>(IServiceProvider serviceProvider)
        where TDbContext : SmartSoftwareDbContext<TDbContext>
    {
        var context = DbContextCreationContext.Current;
        if (context != null)
        {
            return context;
        }

        var connectionStringName = ConnectionStringNameAttribute.GetConnStringName<TDbContext>();
        var connectionString = ResolveConnectionString<TDbContext>(serviceProvider, connectionStringName);

        return new DbContextCreationContext(
            connectionStringName,
            connectionString
        );
    }

    private static string ResolveConnectionString<TDbContext>(
        IServiceProvider serviceProvider,
        string connectionStringName)
    {
        // Use DefaultConnectionStringResolver.Resolve when we remove IConnectionStringResolver.Resolve
#pragma warning disable 618
        var connectionStringResolver = serviceProvider.GetRequiredService<IConnectionStringResolver>();
        var currentTenant = serviceProvider.GetRequiredService<ICurrentTenant>();

        // Multi-tenancy unaware contexts should always use the host connection string
        if (typeof(TDbContext).IsDefined(typeof(IgnoreMultiTenancyAttribute), false))
        {
            using (currentTenant.Change(null))
            {
                return connectionStringResolver.Resolve(connectionStringName);
            }
        }

        return connectionStringResolver.Resolve(connectionStringName);
#pragma warning restore 618
    }
}
