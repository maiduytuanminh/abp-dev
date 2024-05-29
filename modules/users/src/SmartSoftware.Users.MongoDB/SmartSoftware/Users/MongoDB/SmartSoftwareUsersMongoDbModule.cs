using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace SmartSoftware.Users.MongoDB;

[DependsOn(
    typeof(SmartSoftwareUsersDomainModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class SmartSoftwareUsersMongoDbModule : SmartSoftwareModule
{

}
