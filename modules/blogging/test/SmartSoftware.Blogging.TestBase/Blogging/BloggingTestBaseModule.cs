using Microsoft.Extensions.DependencyInjection;
using SmartSoftware;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.Blogging
{
    [DependsOn(
        typeof(BloggingDomainModule),
        typeof(SmartSoftwareTestBaseModule),
        typeof(SmartSoftwareAutofacModule)
        )]
    public class BloggingTestBaseModule : SmartSoftwareModule
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
                    .GetRequiredService<BloggingTestDataBuilder>()
                    .Build();
            }
        }
    }
}
