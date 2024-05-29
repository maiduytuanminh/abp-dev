using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;
using SmartSoftware.Users.MongoDB;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.Users;

namespace SmartSoftware.Blogging.MongoDB
{
    [DependsOn(
        typeof(BloggingDomainModule),
        typeof(SmartSoftwareMongoDbModule),
        typeof(SmartSoftwareUsersMongoDbModule)
    )]
    public class BloggingMongoDbModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<BloggingMongoDbContext>(options =>
            {
                options.AddRepository<Blog, MongoBlogRepository>();
                options.AddRepository<BlogUser, MongoBlogUserRepository>();
                options.AddRepository<Post, MongoPostRepository>();
                options.AddRepository<Tag, MongoTagRepository>();
                options.AddRepository<Comment, MongoCommentRepository>();
            });
        }
    }
}
