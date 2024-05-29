using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB.TestApp.FourthContext;
using SmartSoftware.MongoDB.TestApp.ThirdDbContext;
using SmartSoftware.Threading;

namespace SmartSoftware.MongoDB.TestApp.SecondContext;

[DependsOn(typeof(SmartSoftwareMongoDbModule))]
public class SmartSoftwareMongoDbTestSecondContextModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<SecondDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        context.Services.AddMongoDbContext<ThirdDbContext.ThirdDbContext>(options =>
        {
            options.AddDefaultRepositories<IThirdDbContext>();
        });

        context.Services.AddMongoDbContext<FourthDbContext>(options =>
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
