using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Threading;

namespace SmartSoftware.BlobStoring;

[DependsOn(
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareThreadingModule)
    )]
public class SmartSoftwareBlobStoringModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(
            typeof(IBlobContainer<>),
            typeof(BlobContainer<>)
        );

        context.Services.AddTransient(
            typeof(IBlobContainer),
            serviceProvider => serviceProvider
                .GetRequiredService<IBlobContainer<DefaultContainer>>()
        );
    }
}
