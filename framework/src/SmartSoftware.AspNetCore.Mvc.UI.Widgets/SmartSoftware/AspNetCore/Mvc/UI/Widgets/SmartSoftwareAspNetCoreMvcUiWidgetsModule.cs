using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore.Mvc.UI.Widgets;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule),
    typeof(SmartSoftwareAspNetCoreMvcUiBundlingModule)
)]
public class SmartSoftwareAspNetCoreMvcUiWidgetsModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareAspNetCoreMvcUiWidgetsModule).Assembly);
        });

        AutoAddWidgets(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<DefaultViewComponentHelper>();

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAspNetCoreMvcUiWidgetsModule>();
        });
    }

    private static void AutoAddWidgets(IServiceCollection services)
    {
        var widgetTypes = new List<Type>();

        services.OnRegistered(context =>
        {
            if (WidgetAttribute.IsWidget(context.ImplementationType))
            {
                widgetTypes.Add(context.ImplementationType);
            }
        });

        services.Configure<SmartSoftwareWidgetOptions>(options =>
        {
            foreach (var widgetType in widgetTypes)
            {
                options.Widgets.Add(new WidgetDefinition(widgetType));
            }
        });
    }
}
