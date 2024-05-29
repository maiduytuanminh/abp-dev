using SmartSoftware.EventBus;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Users;

//TODO: Consider to (somehow) move this to the framework to the same assemblily of ICurrentUser!

[DependsOn(
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareEventBusModule)
    )]
public class SmartSoftwareUsersAbstractionModule : SmartSoftwareModule
{

}
