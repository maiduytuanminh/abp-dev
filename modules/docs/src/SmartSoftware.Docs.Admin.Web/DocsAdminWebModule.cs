using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AutoMapper;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Modularity;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Docs.Admin.Navigation;
using SmartSoftware.Docs.Localization;

namespace SmartSoftware.Docs.Admin
{
    [DependsOn(
        typeof(DocsAdminApplicationContractsModule),
        typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule)
        )]
    public class DocsAdminWebModule : SmartSoftwareModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(DocsResource), typeof(DocsAdminWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DocsAdminWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new DocsMenuContributor());
            });

            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocsAdminWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<DocsAdminWebModule>();
            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsAdminWebAutoMapperProfile>(validate: true);
            });

            Configure<DynamicJavaScriptProxyOptions>(options =>
            {
                options.DisableModule(DocsAdminRemoteServiceConsts.ModuleName);
            });
        }
    }
}
