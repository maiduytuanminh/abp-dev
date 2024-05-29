using Microsoft.Extensions.DependencyInjection;
using SmartSoftware;
using SmartSoftware.Authorization;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(SmartSoftwareAutofacModule),
        typeof(SmartSoftwareTestBaseModule),
        typeof(SmartSoftwareAuthorizationModule),
        typeof(DocsDomainModule)
        )]
    public class DocsTestBaseModule : SmartSoftwareModule
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
                AsyncHelper.RunSync(() => scope.ServiceProvider
                    .GetRequiredService<DocsTestDataBuilder>()
                    .BuildAsync());
            }
        }
    }
}
