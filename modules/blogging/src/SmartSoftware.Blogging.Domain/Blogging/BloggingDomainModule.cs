using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AutoMapper;
using SmartSoftware.Caching;
using SmartSoftware.Domain;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.Modularity;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Tagging;

namespace SmartSoftware.Blogging
{
    [DependsOn(
        typeof(BloggingDomainSharedModule),
        typeof(SmartSoftwareDddDomainModule),
        typeof(SmartSoftwareAutoMapperModule),
        typeof(SmartSoftwareCachingModule)
    )]
    public class BloggingDomainModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<BloggingDomainModule>();

            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<BloggingDomainMappingProfile>(validate: true);
            });

            Configure<SmartSoftwareDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Blog, BlogEto>(typeof(BloggingDomainModule));
                options.EtoMappings.Add<Comment, CommentEto>(typeof(BloggingDomainModule));
                options.EtoMappings.Add<Post, PostEto>(typeof(BloggingDomainModule));
                options.EtoMappings.Add<Tag, TagEto>(typeof(BloggingDomainModule));
            });
        }
    }
}
