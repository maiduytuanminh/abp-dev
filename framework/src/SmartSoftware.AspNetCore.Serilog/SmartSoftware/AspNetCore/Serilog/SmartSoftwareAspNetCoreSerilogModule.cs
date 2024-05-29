using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.AspNetCore.Serilog;

[DependsOn(
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareAspNetCoreModule)
)]
public class SmartSoftwareAspNetCoreSerilogModule : SmartSoftwareModule
{
}
