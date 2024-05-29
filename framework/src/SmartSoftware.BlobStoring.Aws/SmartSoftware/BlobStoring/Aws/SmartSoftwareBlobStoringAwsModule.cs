using SmartSoftware.Caching;
using SmartSoftware.Modularity;

namespace SmartSoftware.BlobStoring.Aws;

[DependsOn(typeof(SmartSoftwareBlobStoringModule),
    typeof(SmartSoftwareCachingModule))]
public class SmartSoftwareBlobStoringAwsModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}
