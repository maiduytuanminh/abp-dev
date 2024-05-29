using SmartSoftware.Application;
using SmartSoftware.Authorization;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.FeatureManagement;

[DependsOn(
    typeof(SmartSoftwareFeatureManagementDomainSharedModule),
    typeof(SmartSoftwareDddApplicationContractsModule),
    typeof(SmartSoftwareAuthorizationAbstractionsModule),
    typeof(SmartSoftwareJsonModule)
    )]
public class SmartSoftwareFeatureManagementApplicationContractsModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareFeatureManagementApplicationContractsModule>();
        });
    }
}