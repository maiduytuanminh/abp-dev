using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftware.Users.EntityFrameworkCore;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Comments;
using SmartSoftware.CmsKit.GlobalResources;
using SmartSoftware.CmsKit.MediaDescriptors;
using SmartSoftware.CmsKit.Pages;
using SmartSoftware.CmsKit.Ratings;
using SmartSoftware.CmsKit.Reactions;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.EntityFrameworkCore;

[DependsOn(
    typeof(CmsKitDomainModule),
    typeof(SmartSoftwareUsersEntityFrameworkCoreModule),
    typeof(SmartSoftwareEntityFrameworkCoreModule)
)]
public class CmsKitEntityFrameworkCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<CmsKitDbContext>(options =>
        {
            options.AddRepository<CmsUser, EfCoreCmsUserRepository>();
            options.AddRepository<UserReaction, EfCoreUserReactionRepository>();
            options.AddRepository<Comment, EfCoreCommentRepository>();
            options.AddRepository<Rating, EfCoreRatingRepository>();
            options.AddRepository<Tag, EfCoreTagRepository>();
            options.AddRepository<EntityTag, EfCoreEntityTagRepository>();
            options.AddRepository<Page, EfCorePageRepository>();
            options.AddRepository<Blog, EfCoreBlogRepository>();
            options.AddRepository<BlogPost, EfCoreBlogPostRepository>();
            options.AddRepository<BlogFeature, EfCoreBlogFeatureRepository>();
            options.AddRepository<MediaDescriptor, EfCoreMediaDescriptorRepository>();
            options.AddRepository<GlobalResource, EfCoreGlobalResourceRepository>();
        });
    }
}
