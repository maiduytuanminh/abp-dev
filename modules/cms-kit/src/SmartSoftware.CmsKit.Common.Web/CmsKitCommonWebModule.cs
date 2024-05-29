using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AutoMapper;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.CmsKit.Reactions;
using SmartSoftware.CmsKit.Web.Icons;
using Markdig;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware.CmsKit.Web;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule),
    typeof(CmsKitCommonApplicationContractsModule),
    typeof(SmartSoftwareAutoMapperModule)
    )]
public class CmsKitCommonWebModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<CmsKitUiOptions>(options =>
        {
            options.ReactionIcons[StandardReactions.Smile] = new LocalizableIconDictionary("fas fa-smile text-warning");
            options.ReactionIcons[StandardReactions.ThumbsUp] = new LocalizableIconDictionary("fa fa-thumbs-up text-primary");
            options.ReactionIcons[StandardReactions.Confused] = new LocalizableIconDictionary("fas fa-surprise text-warning");
            options.ReactionIcons[StandardReactions.Eyes] = new LocalizableIconDictionary("fas fa-meh-rolling-eyes text-warning");
            options.ReactionIcons[StandardReactions.Heart] = new LocalizableIconDictionary("fa fa-heart text-danger");
            options.ReactionIcons[StandardReactions.HeartBroken] = new LocalizableIconDictionary("fas fa-heart-broken text-danger");
            options.ReactionIcons[StandardReactions.Wink] = new LocalizableIconDictionary("fas fa-grin-wink text-warning");
            options.ReactionIcons[StandardReactions.Pray] = new LocalizableIconDictionary("fas fa-praying-hands text-info");
            options.ReactionIcons[StandardReactions.Rocket] = new LocalizableIconDictionary("fa fa-rocket text-success");
            options.ReactionIcons[StandardReactions.ThumbsDown] = new LocalizableIconDictionary("fa fa-thumbs-down text-secondary");
            options.ReactionIcons[StandardReactions.Victory] = new LocalizableIconDictionary("fas fa-hand-peace text-warning");
            options.ReactionIcons[StandardReactions.Rock] = new LocalizableIconDictionary("fas fa-hand-rock text-warning");
        });
        
        context.Services
                    .AddSingleton(_ => new MarkdownPipelineBuilder()
                        .UseAutoLinks()
                        .UseBootstrap()
                        .UseGridTables()
                        .UsePipeTables()
                        .Build());

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitCommonWebModule>();
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(CmsKitCommonRemoteServiceConsts.ModuleName);
        });
    }
}

