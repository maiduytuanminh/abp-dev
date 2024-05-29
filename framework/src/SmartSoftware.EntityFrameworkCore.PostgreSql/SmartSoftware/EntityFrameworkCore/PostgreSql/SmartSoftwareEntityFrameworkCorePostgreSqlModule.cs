using SmartSoftware.Guids;
using SmartSoftware.Modularity;

namespace SmartSoftware.EntityFrameworkCore.PostgreSql;

[DependsOn(
    typeof(SmartSoftwareEntityFrameworkCoreModule)
    )]
public class SmartSoftwareEntityFrameworkCorePostgreSqlModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareSequentialGuidGeneratorOptions>(options =>
        {
            if (options.DefaultSequentialGuidType == null)
            {
                options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsString;
            }
        });
    }
}
