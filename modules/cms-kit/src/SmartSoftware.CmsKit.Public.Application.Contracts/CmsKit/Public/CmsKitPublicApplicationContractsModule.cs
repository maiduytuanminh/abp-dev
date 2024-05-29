using SmartSoftware.Modularity;
using SmartSoftware.EventBus;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Threading;
using SmartSoftware.CmsKit.Public.Comments;
using SmartSoftware.CmsKit.Public.GlobalResources;

namespace SmartSoftware.CmsKit.Public;

[DependsOn(
    typeof(CmsKitCommonApplicationContractsModule),
    typeof(SmartSoftwareEventBusModule)
    )]
public class CmsKitPublicApplicationContractsModule : SmartSoftwareModule
{
    private readonly static OneTimeRunner OneTimeRunner = new OneTimeRunner();
    
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.Comment,
                getApiTypes: new[] { typeof(CommentDto) , typeof(CommentWithDetailsDto) },
                createApiTypes: new []{ typeof(CreateCommentInput) },
                updateApiTypes: new []{ typeof(UpdateCommentInput) }
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                CmsKitModuleExtensionConsts.ModuleName,
                CmsKitModuleExtensionConsts.EntityNames.GlobalResource,
                getApiTypes: new[] { typeof(GlobalResourceDto) }
            );
        });
    }
}
