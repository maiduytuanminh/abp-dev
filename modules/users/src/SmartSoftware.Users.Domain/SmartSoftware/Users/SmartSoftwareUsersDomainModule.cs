using SmartSoftware.Domain;
using SmartSoftware.Modularity;

namespace SmartSoftware.Users;

[DependsOn(
    typeof(SmartSoftwareUsersDomainSharedModule),
    typeof(SmartSoftwareUsersAbstractionModule),
    typeof(SmartSoftwareDddDomainModule)
    )]
public class SmartSoftwareUsersDomainModule : SmartSoftwareModule
{

}
