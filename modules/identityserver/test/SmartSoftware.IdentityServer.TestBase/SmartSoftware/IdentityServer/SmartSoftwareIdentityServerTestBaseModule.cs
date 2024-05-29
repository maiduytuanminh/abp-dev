using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.IdentityServer;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareIdentityServerDomainModule)
)]
public class SmartSoftwareIdentityServerTestBaseModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<SmartSoftwareIdentityServerBuilderOptions>(options =>
        {
            options.AddDeveloperSigningCredential = false;
        });

        PreConfigure<IIdentityServerBuilder>(identityServerBuilder =>
        {
            identityServerBuilder.AddDeveloperSigningCredential(false, System.Guid.NewGuid().ToString());
        });
    }
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
            AsyncHelper.RunSync(() => scope.ServiceProvider
                .GetRequiredService<SmartSoftwareIdentityServerTestDataBuilder>()
                .BuildAsync());
        }
    }
}
