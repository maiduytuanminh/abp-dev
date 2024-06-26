﻿using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.AuditLogging;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAuditLoggingDomainModule))]
public class SmartSoftwareAuditLoggingTestBaseModule : SmartSoftwareModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            scope.ServiceProvider
                .GetRequiredService<AuditingTestDataBuilder>()
                .Build();
        }
    }
}
