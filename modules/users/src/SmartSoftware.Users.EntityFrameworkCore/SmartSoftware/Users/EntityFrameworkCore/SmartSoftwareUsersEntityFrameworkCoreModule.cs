using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.Users.EntityFrameworkCore;

[DependsOn(
    typeof(SmartSoftwareUsersDomainModule),
    typeof(SmartSoftwareEntityFrameworkCoreModule)
    )]
public class SmartSoftwareUsersEntityFrameworkCoreModule : SmartSoftwareModule
{

}
