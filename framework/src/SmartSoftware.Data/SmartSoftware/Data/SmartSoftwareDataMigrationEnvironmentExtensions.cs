using System;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Data;

public static class SmartSoftwareDataMigrationEnvironmentExtensions
{
    public static void AddDataMigrationEnvironment(this SmartSoftwareApplicationCreationOptions options, SmartSoftwareDataMigrationEnvironment? environment = null)
    {
        options.Services.AddDataMigrationEnvironment(environment ?? new SmartSoftwareDataMigrationEnvironment());
    }

    public static void AddDataMigrationEnvironment(this IServiceCollection services, SmartSoftwareDataMigrationEnvironment? environment = null)
    {
        services.AddObjectAccessor<SmartSoftwareDataMigrationEnvironment>(environment ?? new SmartSoftwareDataMigrationEnvironment());
    }

    public static SmartSoftwareDataMigrationEnvironment? GetDataMigrationEnvironment(this IServiceCollection services)
    {
        return services.GetObjectOrNull<SmartSoftwareDataMigrationEnvironment>();
    }

    public static bool IsDataMigrationEnvironment(this IServiceCollection services)
    {
        return services.GetDataMigrationEnvironment() != null;
    }

    public static SmartSoftwareDataMigrationEnvironment? GetDataMigrationEnvironment(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IObjectAccessor<SmartSoftwareDataMigrationEnvironment>>()?.Value;
    }

    public static bool IsDataMigrationEnvironment(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetDataMigrationEnvironment() != null;
    }
}
