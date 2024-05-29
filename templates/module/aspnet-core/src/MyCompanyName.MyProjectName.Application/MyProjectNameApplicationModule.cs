using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AutoMapper;
using SmartSoftware.Modularity;
using SmartSoftware.Application;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(MyProjectNameDomainModule),
    typeof(MyProjectNameApplicationContractsModule),
    typeof(SmartSoftwareDddApplicationModule),
    typeof(SmartSoftwareAutoMapperModule)
    )]
public class MyProjectNameApplicationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<MyProjectNameApplicationModule>();
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddMaps<MyProjectNameApplicationModule>(validate: true);
        });
    }
}
