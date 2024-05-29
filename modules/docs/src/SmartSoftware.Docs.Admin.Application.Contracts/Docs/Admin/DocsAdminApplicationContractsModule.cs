using SmartSoftware.Application;
using SmartSoftware.Authorization;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Docs.Localization;

namespace SmartSoftware.Docs.Admin
{
    [DependsOn(
        typeof(DocsDomainSharedModule),
        typeof(SmartSoftwareDddApplicationContractsModule),
        typeof(SmartSoftwareAuthorizationAbstractionsModule)
        )]
    public class DocsAdminApplicationContractsModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocsAdminApplicationContractsModule>();
            });

            Configure<SmartSoftwareLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DocsResource>()
                    .AddVirtualJson("SmartSoftware/Docs/Admin/Localization/Resources/Docs/ApplicationContracts");
            });
        }
    }
}
