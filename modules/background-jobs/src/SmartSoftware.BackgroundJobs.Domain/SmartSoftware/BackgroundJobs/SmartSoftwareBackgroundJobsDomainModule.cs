using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AutoMapper;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsDomainSharedModule),
    typeof(SmartSoftwareBackgroundJobsModule),
    typeof(SmartSoftwareAutoMapperModule)
    )]
public class SmartSoftwareBackgroundJobsDomainModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareBackgroundJobsDomainModule>();
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<BackgroundJobsDomainAutoMapperProfile>(validate: true);
        });
    }
}
