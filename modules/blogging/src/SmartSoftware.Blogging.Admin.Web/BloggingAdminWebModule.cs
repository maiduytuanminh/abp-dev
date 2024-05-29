using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AutoMapper;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Modularity;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.Blogging.Admin
{
    [DependsOn(
        typeof(BloggingAdminApplicationContractsModule),
        typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule),
        typeof(SmartSoftwareAspNetCoreMvcUiBundlingModule),
        typeof(SmartSoftwareAutoMapperModule)
    )]
    public class BloggingAdminWebModule : SmartSoftwareModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(BloggingResource), typeof(BloggingAdminWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BloggingAdminWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new BloggingAdminMenuContributor());
            });

            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BloggingAdminWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<BloggingAdminWebModule>();
            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<SmartSoftwareBloggingAdminWebAutoMapperProfile>(validate: true);
            });

            Configure<DynamicJavaScriptProxyOptions>(options =>
            {
                options.DisableModule(BloggingAdminRemoteServiceConsts.ModuleName);
            });
        }
    }
}
