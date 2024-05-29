using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.Ui.LayoutHooks;
using SmartSoftware.AspNetCore.Mvc.UI.Packages;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Prismjs;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AutoMapper;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Docs.Bundling;
using SmartSoftware.Docs.HtmlConverting;
using SmartSoftware.Docs.Localization;
using SmartSoftware.Docs.Markdown;
using SmartSoftware.Docs.Pages.Shared.Components.Head;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(DocsApplicationContractsModule),
        typeof(SmartSoftwareAutoMapperModule),
        typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule),
        typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule),
        typeof(SmartSoftwareAspNetCoreMvcUiPackagesModule),
        typeof(SmartSoftwareAspNetCoreMvcUiBundlingModule)
        )]
    public class DocsWebModule : SmartSoftwareModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(DocsResource), typeof(DocsWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DocsWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocsWebModule>();
            });

            var configuration = context.Services.GetConfiguration();

            Configure<RazorPagesOptions>(options =>
            {
                var docsOptions = context.Services
                    .GetRequiredServiceLazy<IOptions<DocsUiOptions>>()
                    .Value.Value;

                var routePrefix = docsOptions.RoutePrefix;

                options.Conventions.AddPageRoute("/Documents/Project/Index", routePrefix + "{projectName}");
                options.Conventions.AddPageRoute("/Documents/Project/Index", routePrefix + "{languageCode}/{projectName}");
                options.Conventions.AddPageRoute("/Documents/Project/Index", routePrefix + "{languageCode}/{projectName}/{version}/{*documentName}");
                options.Conventions.AddPageRoute("/Documents/Search", routePrefix + "search/{languageCode}/{projectName}/{version}");
            });

            context.Services.AddAutoMapperObjectMapper<DocsWebModule>();
            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsWebAutoMapperProfile>(validate: true);
            });

            Configure<DocumentToHtmlConverterOptions>(options =>
            {
                options.Converters[MarkdownDocumentToHtmlConverter.Type] = typeof(MarkdownDocumentToHtmlConverter);
            });

            Configure<SmartSoftwareBundleContributorOptions>(options =>
            {
                options
                    .Extensions<PrismjsStyleBundleContributor>()
                    .Add<PrismjsStyleBundleContributorDocsExtension>();

                options
                    .Extensions<PrismjsScriptBundleContributor>()
                    .Add<PrismjsScriptBundleContributorDocsExtension>();
            });

            Configure<DynamicJavaScriptProxyOptions>(options =>
            {
                options.DisableModule(DocsRemoteServiceConsts.ModuleName);
            });

            Configure<SmartSoftwareLayoutHookOptions>(options =>
            {
                options.Add(LayoutHooks.Head.Last, typeof(HeadViewComponent));
            });
        }
    }
}
