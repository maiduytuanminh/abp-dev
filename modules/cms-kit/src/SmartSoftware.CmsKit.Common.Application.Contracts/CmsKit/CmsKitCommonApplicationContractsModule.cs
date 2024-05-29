using SmartSoftware.Application;
using SmartSoftware.Authorization;
using SmartSoftware.Caching;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Threading;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitDomainSharedModule),
    typeof(SmartSoftwareDddApplicationContractsModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(SmartSoftwareCachingModule)
)]
public class CmsKitCommonApplicationContractsModule : SmartSoftwareModule
{
    private readonly static OneTimeRunner OneTimeRunner = new OneTimeRunner();
    
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.BlogFeature,
                getApiTypes: new[] { typeof(BlogFeatureDto) }
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.BlogPost,
                getApiTypes: new[] { typeof(BlogPostCommonDto) }
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.MenuItem,
                getApiTypes: new[] { typeof(MenuItemDto) }
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.Tag,
                getApiTypes: new[] { typeof(TagDto) }
            );
            
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.CmsUser,
                getApiTypes: new[] { typeof(CmsUserDto) }
            );
        });
    }
}
