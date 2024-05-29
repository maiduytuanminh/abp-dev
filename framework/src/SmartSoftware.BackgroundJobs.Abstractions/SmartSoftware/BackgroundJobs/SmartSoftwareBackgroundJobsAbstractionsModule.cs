using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Reflection;

namespace SmartSoftware.BackgroundJobs;

[DependsOn(
    typeof(SmartSoftwareJsonModule),
    typeof(SmartSoftwareMultiTenancyAbstractionsModule)
    )]
public class SmartSoftwareBackgroundJobsAbstractionsModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        RegisterJobs(context.Services);
    }

    private static void RegisterJobs(IServiceCollection services)
    {
        var jobTypes = new List<Type>();

        services.OnRegistered(context =>
        {
            if (ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(IBackgroundJob<>)) ||
                ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(IAsyncBackgroundJob<>)))
            {
                jobTypes.Add(context.ImplementationType);
            }
        });

        services.Configure<SmartSoftwareBackgroundJobOptions>(options =>
        {
            foreach (var jobType in jobTypes)
            {
                options.AddJob(jobType);
            }
        });
    }
}
