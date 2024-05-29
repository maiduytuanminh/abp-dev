using System;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Hosting;

public static class SmartSoftwareHostingEnvironmentExtensions
{
    public static IConfigurationRoot BuildConfiguration(
        this IWebHostEnvironment env,
        SmartSoftwareConfigurationBuilderOptions? options = null)
    {
        options ??= new SmartSoftwareConfigurationBuilderOptions();

        if (options.BasePath.IsNullOrEmpty())
        {
            options.BasePath = env.ContentRootPath;
        }

        if (options.EnvironmentName.IsNullOrEmpty())
        {
            options.EnvironmentName = env.EnvironmentName;
        }

        return ConfigurationHelper.BuildConfiguration(options);
    }
}
