using SmartSoftware.Application;
using SmartSoftware.Emailing;
using SmartSoftware.Modularity;
using SmartSoftware.Timing;
using SmartSoftware.Users;

namespace SmartSoftware.SettingManagement;

[DependsOn(
    typeof(SmartSoftwareDddApplicationModule),
    typeof(SmartSoftwareSettingManagementDomainModule),
    typeof(SmartSoftwareSettingManagementApplicationContractsModule),
    typeof(SmartSoftwareEmailingModule),
    typeof(SmartSoftwareTimingModule),
    typeof(SmartSoftwareUsersAbstractionModule)
)]
public class SmartSoftwareSettingManagementApplicationModule : SmartSoftwareModule
{
}
