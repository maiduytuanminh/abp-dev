using SmartSoftware.Application;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc;

[DependsOn(
    typeof(SmartSoftwareDddApplicationContractsModule)
    )]
public class SmartSoftwareAspNetCoreMvcContractsModule : SmartSoftwareModule
{

}
