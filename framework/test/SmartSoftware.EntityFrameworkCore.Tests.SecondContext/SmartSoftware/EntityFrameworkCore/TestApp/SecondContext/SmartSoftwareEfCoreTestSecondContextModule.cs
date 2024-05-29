using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore.TestApp.FourthContext;
using SmartSoftware.EntityFrameworkCore.TestApp.ThirdDbContext;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.EntityFrameworkCore.TestApp.SecondContext;

[DependsOn(typeof(SmartSoftwareEntityFrameworkCoreModule))]
public class SmartSoftwareEfCoreTestSecondContextModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<SecondDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        context.Services.AddSmartSoftwareDbContext<ThirdDbContext.ThirdDbContext>(options =>
        {
            options.AddDefaultRepositories<IThirdDbContext>();
        });

        context.Services.AddSmartSoftwareDbContext<FourthDbContext>(options =>
        {
            options.AddDefaultRepositories<IFourthDbContext>();
        });
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
                .GetRequiredService<SecondContextTestDataBuilder>()
                .BuildAsync());
        }
    }
}
