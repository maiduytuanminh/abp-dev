using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization;
using SmartSoftware.Autofac;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.Identity;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareIdentityDomainModule),
    typeof(SmartSoftwareAuthorizationModule)
    )]
public class SmartSoftwareIdentityTestBaseModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            AsyncHelper.RunSync(async () =>
            {
                await dataSeeder.SeedAsync();
                await scope.ServiceProvider
                    .GetRequiredService<SmartSoftwareIdentityTestDataBuilder>()
                    .Build();
            });
        }
    }
}
