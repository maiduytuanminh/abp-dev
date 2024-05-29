using SmartSoftware.BlobStoring;
using SmartSoftware.Domain;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Threading;
using SmartSoftware.Users;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Comments;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Localization;
using SmartSoftware.CmsKit.MediaDescriptors;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Pages;
using SmartSoftware.CmsKit.Ratings;
using SmartSoftware.CmsKit.Reactions;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitDomainSharedModule),
    typeof(SmartSoftwareUsersDomainModule),
    typeof(SmartSoftwareDddDomainModule),
    typeof(SmartSoftwareBlobStoringModule)
)]
public class CmsKitDomainModule : SmartSoftwareModule
{
    private readonly static OneTimeRunner OneTimeRunner = new OneTimeRunner();
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        if (GlobalFeatureManager.Instance.IsEnabled<ReactionsFeature>())
        {
            Configure<CmsKitReactionOptions>(options =>
            {
                if (GlobalFeatureManager.Instance.IsEnabled<BlogsFeature>())
                {
                    options.EntityTypes.Add(
                        new ReactionEntityTypeDefinition(
                            BlogPostConsts.EntityType,
                            reactions: new[]
                            {
                                    new ReactionDefinition(StandardReactions.Smile),
                                    new ReactionDefinition(StandardReactions.ThumbsUp),
                                    new ReactionDefinition(StandardReactions.ThumbsDown),
                                    new ReactionDefinition(StandardReactions.Confused),
                                    new ReactionDefinition(StandardReactions.Eyes),
                                    new ReactionDefinition(StandardReactions.Heart),
                                    new ReactionDefinition(StandardReactions.HeartBroken),
                                    new ReactionDefinition(StandardReactions.Wink),
                                    new ReactionDefinition(StandardReactions.Pray),
                                    new ReactionDefinition(StandardReactions.Rocket),
                                    new ReactionDefinition(StandardReactions.Victory),
                                    new ReactionDefinition(StandardReactions.Rock),
                            }));
                }

                if (GlobalFeatureManager.Instance.IsEnabled<CommentsFeature>())
                {
                    options.EntityTypes.Add(
                        new ReactionEntityTypeDefinition(
                            CommentConsts.EntityType,
                            reactions: new[]
                            {
                                    new ReactionDefinition(StandardReactions.ThumbsUp),
                                    new ReactionDefinition(StandardReactions.ThumbsDown),
                            }));
                }
            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<RatingsFeature>())
        {
            Configure<CmsKitRatingOptions>(options =>
            {
                if (GlobalFeatureManager.Instance.IsEnabled<BlogsFeature>())
                {
                    options.EntityTypes.Add(new RatingEntityTypeDefinition(BlogPostConsts.EntityType));
                }

                    // TODO: Define entity types here which can be rated.
                });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<TagsFeature>())
        {
            // TODO: Configure TagEntityTypes here...
        }
    }
    
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.Blog,
                typeof(Blog)
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.BlogPost,
                typeof(BlogPost)
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.BlogFeature,
                typeof(BlogFeature)
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.MediaDescriptor,
                typeof(MediaDescriptor)
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.Page,
                typeof(Page)
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.Tag,
                typeof(Tag)
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.Comment,
                typeof(Comment)
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.MenuItem,
                typeof(MenuItem)
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.CmsUser,
                typeof(CmsUser)
            );
        });
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CmsKitResource>(name);
    }
}
