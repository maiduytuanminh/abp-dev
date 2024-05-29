using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.Application;
using SmartSoftware.AutoMapper;
using SmartSoftware.Caching;
using SmartSoftware.Modularity;
using SmartSoftware.Docs.Documents;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(DocsDomainModule),
        typeof(DocsApplicationContractsModule),
        typeof(SmartSoftwareCachingModule),
        typeof(SmartSoftwareAutoMapperModule),
        typeof(SmartSoftwareDddApplicationModule)
        )]
    public class DocsApplicationModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DocsApplicationModule>();
            
            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsApplicationAutoMapperProfile>(validate: true);
            });
            
            context.Services.TryAddSingleton<INavigationTreePostProcessor>(NullNavigationTreePostProcessor.Instance);
        }
    }
}
