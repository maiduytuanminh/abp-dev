using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Application;
using SmartSoftware.AutoMapper;
using SmartSoftware.Caching;
using SmartSoftware.Modularity;

namespace SmartSoftware.Docs.Admin
{
    [DependsOn(
        typeof(DocsDomainModule),
        typeof(DocsAdminApplicationContractsModule),
        typeof(SmartSoftwareCachingModule),
        typeof(SmartSoftwareAutoMapperModule),
        typeof(SmartSoftwareDddApplicationModule)
    )]
    public class DocsAdminApplicationModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DocsAdminApplicationModule>();
            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsAdminApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
