using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SmartSoftware;
using SmartSoftware.Authorization;
using SmartSoftware.Autofac;
using SmartSoftware.BlobStoring;
using SmartSoftware.Data;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(CmsKitDomainModule)
    )]
public class CmsKitTestBaseModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            GlobalFeatureManager.Instance.Modules.CmsKit().EnableAll();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IBlobProvider>(Substitute.For<FakeBlobProvider>());

        Configure<SmartSoftwareBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.ProviderType = typeof(FakeBlobProvider);
            });
        });

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
