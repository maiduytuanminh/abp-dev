using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;
using SmartSoftware.Users.MongoDB;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Comments;
using SmartSoftware.CmsKit.GlobalResources;
using SmartSoftware.CmsKit.MediaDescriptors;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.MongoDB.Blogs;
using SmartSoftware.CmsKit.MongoDB.Comments;
using SmartSoftware.CmsKit.MongoDB.GlobalResources;
using SmartSoftware.CmsKit.MongoDB.MediaDescriptors;
using SmartSoftware.CmsKit.MongoDB.Menus;
using SmartSoftware.CmsKit.MongoDB.Pages;
using SmartSoftware.CmsKit.MongoDB.Ratings;
using SmartSoftware.CmsKit.MongoDB.Reactions;
using SmartSoftware.CmsKit.MongoDB.Tags;
using SmartSoftware.CmsKit.MongoDB.Users;
using SmartSoftware.CmsKit.Pages;
using SmartSoftware.CmsKit.Ratings;
using SmartSoftware.CmsKit.Reactions;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.MongoDB;

[DependsOn(
    typeof(CmsKitDomainModule),
    typeof(SmartSoftwareUsersMongoDbModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class CmsKitMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<CmsKitMongoDbContext>(options =>
        {
            options.AddRepository<CmsUser, MongoCmsUserRepository>();
            options.AddRepository<UserReaction, MongoUserReactionRepository>();
            options.AddRepository<Comment, MongoCommentRepository>();
            options.AddRepository<Rating, MongoRatingRepository>();
            options.AddRepository<Tag, MongoTagRepository>();
            options.AddRepository<EntityTag, MongoEntityTagRepository>();
            options.AddRepository<Page, MongoPageRepository>();
            options.AddRepository<Blog, MongoBlogRepository>();
            options.AddRepository<BlogPost, MongoBlogPostRepository>();
            options.AddRepository<MediaDescriptor, MongoMediaDescriptorRepository>();
            options.AddRepository<MenuItem, MongoMenuItemRepository>();
            options.AddRepository<GlobalResource, MongoGlobalResourceRepository>();
        });
    }
}
