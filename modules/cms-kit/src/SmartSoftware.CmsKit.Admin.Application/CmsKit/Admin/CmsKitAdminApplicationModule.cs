using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using SmartSoftware.AutoMapper;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Comments;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Localization;
using SmartSoftware.CmsKit.MediaDescriptors;
using SmartSoftware.CmsKit.Pages;
using SmartSoftware.CmsKit.Permissions;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Admin;

[DependsOn(
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(SmartSoftwareAutoMapperModule),
    typeof(CmsKitCommonApplicationModule)
    )]
public class CmsKitAdminApplicationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CmsKitAdminApplicationModule>();

        ConfigureTagOptions();

        ConfigureCommentOptions();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsKitAdminApplicationModule>(validate: true);
        });
    }

    private void ConfigureTagOptions()
    {
        Configure<CmsKitTagOptions>(opts =>
        {
            if (GlobalFeatureManager.Instance.IsEnabled<BlogsFeature>())
            {
                opts.EntityTypes.AddIfNotContains(
                    new TagEntityTypeDefiniton(
                        BlogPostConsts.EntityType,
                        LocalizableString.Create<CmsKitResource>("BlogPost"),
                        createPolicies: new[]
                        {
                                CmsKitAdminPermissions.BlogPosts.Create,
                                CmsKitAdminPermissions.BlogPosts.Update
                        },
                        updatePolicies: new[]
                        {
                                CmsKitAdminPermissions.BlogPosts.Create,
                                CmsKitAdminPermissions.BlogPosts.Update
                        },
                        deletePolicies: new[]
                        {
                                CmsKitAdminPermissions.BlogPosts.Create,
                                CmsKitAdminPermissions.BlogPosts.Update
                        }));
            }
        });

        if (GlobalFeatureManager.Instance.IsEnabled<MediaFeature>())
        {
            Configure<CmsKitMediaOptions>(options =>
            {
                if (GlobalFeatureManager.Instance.IsEnabled<BlogsFeature>())
                {
                    options.EntityTypes.AddIfNotContains(
                        new MediaDescriptorDefinition(
                            BlogPostConsts.EntityType,
                            createPolicies: new[]
                            {
                                    CmsKitAdminPermissions.BlogPosts.Create,
                                    CmsKitAdminPermissions.BlogPosts.Update
                            },
                            deletePolicies: new[]
                            {
                                    CmsKitAdminPermissions.BlogPosts.Create,
                                    CmsKitAdminPermissions.BlogPosts.Update,
                                    CmsKitAdminPermissions.BlogPosts.Delete
                            }));
                }

                if (GlobalFeatureManager.Instance.IsEnabled<PagesFeature>())
                {
                    options.EntityTypes.AddIfNotContains(
                        new MediaDescriptorDefinition(
                            PageConsts.EntityType,
                            createPolicies: new[]
                            {
                                    CmsKitAdminPermissions.Pages.Create,
                                    CmsKitAdminPermissions.Pages.Update
                            },
                            deletePolicies: new[]
                            {
                                    CmsKitAdminPermissions.Pages.Create,
                                    CmsKitAdminPermissions.Pages.Update,
                                    CmsKitAdminPermissions.Pages.Delete
                            }));
                }
            });
        }
    }

    private void ConfigureCommentOptions()
    {
        if (GlobalFeatureManager.Instance.IsEnabled<CommentsFeature>())
        {
            Configure<CmsKitCommentOptions>(options =>
            {
                if (GlobalFeatureManager.Instance.IsEnabled<BlogsFeature>())
                {
                    options.EntityTypes.AddIfNotContains(
                        new CommentEntityTypeDefinition(BlogPostConsts.EntityType));

                }
            });
        }
    }
}
