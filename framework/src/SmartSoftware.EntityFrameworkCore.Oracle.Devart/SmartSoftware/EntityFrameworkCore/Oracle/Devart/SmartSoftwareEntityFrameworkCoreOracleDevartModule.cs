using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Guids;
using SmartSoftware.Modularity;

namespace SmartSoftware.EntityFrameworkCore.Oracle.Devart;

[DependsOn(
    typeof(SmartSoftwareEntityFrameworkCoreModule)
    )]
public class SmartSoftwareEntityFrameworkCoreOracleDevartModule : SmartSoftwareModule
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
