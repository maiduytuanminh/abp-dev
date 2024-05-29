using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.Users;

namespace SmartSoftware.Blogging.EntityFrameworkCore
{
    [DependsOn(
        typeof(BloggingDomainModule),
        typeof(SmartSoftwareEntityFrameworkCoreModule))]
    public class BloggingEntityFrameworkCoreModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSmartSoftwareDbContext<BloggingDbContext>(options =>
            {
                options.AddRepository<Blog, EfCoreBlogRepository>();
                options.AddRepository<BlogUser, EfCoreBlogUserRepository>();
                options.AddRepository<Post, EfCorePostRepository>();
                options.AddRepository<Tag, EfCoreTagRepository>();
                options.AddRepository<Comment, EfCoreCommentRepository>();
            });
        }
    }
}
