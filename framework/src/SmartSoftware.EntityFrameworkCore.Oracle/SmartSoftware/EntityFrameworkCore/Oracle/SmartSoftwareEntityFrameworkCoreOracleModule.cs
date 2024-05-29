using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Guids;
using SmartSoftware.Modularity;

namespace SmartSoftware.EntityFrameworkCore.Oracle;

[DependsOn(typeof(SmartSoftwareEntityFrameworkCoreModule))]
public class SmartSoftwareEntityFrameworkCoreOracleModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareSequentialGuidGeneratorOptions>(options =>
        {
            if (options.DefaultSequentialGuidType == null)
            {
                options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsBinary;
            }
        });
    }
}
