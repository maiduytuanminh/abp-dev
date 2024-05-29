using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Application;
using SmartSoftware.AutoMapper;
using SmartSoftware.Caching;
using SmartSoftware.Modularity;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;

namespace SmartSoftware.Blogging.Admin
{
    [DependsOn(
        typeof(BloggingDomainModule),
        typeof(BloggingAdminApplicationContractsModule),
        typeof(SmartSoftwareCachingModule),
        typeof(SmartSoftwareAutoMapperModule),
        typeof(SmartSoftwareDddApplicationModule)
        )]
    public class BloggingAdminApplicationModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<BloggingAdminApplicationModule>();
            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<BloggingAdminApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
