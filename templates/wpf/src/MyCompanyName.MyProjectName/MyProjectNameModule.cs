using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(SmartSoftwareAutofacModule))]
public class MyProjectNameModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<MainWindow>();
    }
}
