using SmartSoftware.Caching;
using SmartSoftware.Modularity;

namespace SmartSoftware.BlobStoring.Aliyun;

[DependsOn(
    typeof(SmartSoftwareBlobStoringModule),
    typeof(SmartSoftwareCachingModule)
    )]
public class SmartSoftwareBlobStoringAliyunModule : SmartSoftwareModule
{

}
