using System;
using Microsoft.Extensions.Hosting;

namespace SmartSoftware;

public static class SmartSoftwareHostEnvironmentExtensions
{
    public static bool IsDevelopment(this ISmartSoftwareHostEnvironment hostEnvironment)
    {
        Check.NotNull(hostEnvironment, nameof(hostEnvironment));

        return hostEnvironment.IsEnvironment(Environments.Development);
    }

    public static bool IsStaging(this ISmartSoftwareHostEnvironment hostEnvironment)
    {
        Check.NotNull(hostEnvironment, nameof(hostEnvironment));

        return hostEnvironment.IsEnvironment(Environments.Staging);
    }

    public static bool IsProduction(this ISmartSoftwareHostEnvironment hostEnvironment)
    {
        Check.NotNull(hostEnvironment, nameof(hostEnvironment));

        return hostEnvironment.IsEnvironment(Environments.Production);
    }

    public static bool IsEnvironment(this ISmartSoftwareHostEnvironment hostEnvironment, string environmentName)
    {
        Check.NotNull(hostEnvironment, nameof(hostEnvironment));

        return string.Equals(
            hostEnvironment.EnvironmentName,
            environmentName,
            StringComparison.OrdinalIgnoreCase);
    }
}
