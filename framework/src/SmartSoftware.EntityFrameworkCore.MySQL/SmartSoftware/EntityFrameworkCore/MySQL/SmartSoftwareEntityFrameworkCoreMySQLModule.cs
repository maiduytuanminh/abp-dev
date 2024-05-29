using SmartSoftware.Guids;
using SmartSoftware.Modularity;

namespace SmartSoftware.EntityFrameworkCore.MySQL;

[DependsOn(
    typeof(SmartSoftwareEntityFrameworkCoreModule)
    )]
public class SmartSoftwareEntityFrameworkCoreMySQLModule : SmartSoftwareModule
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
