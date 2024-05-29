using SmartSoftware.Guids;
using SmartSoftware.Modularity;

namespace SmartSoftware.EntityFrameworkCore.SqlServer;

[DependsOn(
    typeof(SmartSoftwareEntityFrameworkCoreModule)
    )]
public class SmartSoftwareEntityFrameworkCoreSqlServerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareSequentialGuidGeneratorOptions>(options =>
        {
            if (options.DefaultSequentialGuidType == null)
            {
                options.DefaultSequentialGuidType = SequentialGuidType.SequentialAtEnd;
            }
        });
    }
}
