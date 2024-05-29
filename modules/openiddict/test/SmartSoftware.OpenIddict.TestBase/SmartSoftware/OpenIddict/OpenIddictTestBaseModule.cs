using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization;
using SmartSoftware.Autofac;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.OpenIddict;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(SmartSoftwareOpenIddictDomainModule)
    )]
public class OpenIddictTestBaseModule : SmartSoftwareModule
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
        AsyncHelper.RunSync(async () =>
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                await scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            }
        });
    }
}
