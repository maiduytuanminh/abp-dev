using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.MyProjectName.Blazor.Menus;
using SmartSoftware.AspNetCore.Components.Web.Theming;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AutoMapper;
using SmartSoftware.Modularity;
using SmartSoftware.UI.Navigation;

namespace MyCompanyName.MyProjectName.Blazor;

[DependsOn(
    typeof(MyProjectNameApplicationContractsModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebThemingModule),
    typeof(SmartSoftwareAutoMapperModule)
    )]
public class MyProjectNameBlazorModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<MyProjectNameBlazorModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<MyProjectNameBlazorAutoMapperProfile>(validate: true);
        });

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MyProjectNameMenuContributor());
        });

        Configure<SmartSoftwareRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(MyProjectNameBlazorModule).Assembly);
        });
    }
}
