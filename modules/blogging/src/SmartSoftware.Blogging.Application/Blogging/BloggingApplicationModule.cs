using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Application;
using SmartSoftware.AutoMapper;
using SmartSoftware.BlobStoring;
using SmartSoftware.Caching;
using SmartSoftware.Modularity;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;

namespace SmartSoftware.Blogging
{
    [DependsOn(
        typeof(BloggingDomainModule),
        typeof(BloggingApplicationContractsModule),
        typeof(SmartSoftwareCachingModule),
        typeof(SmartSoftwareAutoMapperModule),
        typeof(SmartSoftwareBlobStoringModule),
        typeof(SmartSoftwareDddApplicationModule)
        )]
    public class BloggingApplicationModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<BloggingApplicationModule>();
            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<BloggingApplicationAutoMapperProfile>(validate: true);
            });

            Configure<AuthorizationOptions>(options =>
            {
                //TODO: Rename UpdatePolicy/DeletePolicy since it's candidate to conflicts with other modules!
                options.AddPolicy("BloggingUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
                options.AddPolicy("BloggingDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
            });

            context.Services.AddSingleton<IAuthorizationHandler, CommentAuthorizationHandler>();
            context.Services.AddSingleton<IAuthorizationHandler, PostAuthorizationHandler>();

        }
    }
}
