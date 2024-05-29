using SmartSoftware.Modularity;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.Users;

namespace SmartSoftware.SettingManagement;

[DependsOn(
    typeof(SmartSoftwareSettingManagementEntityFrameworkCoreTestModule),
    typeof(SmartSoftwareUsersAbstractionModule))]
public class SmartSoftwareSettingManagementTestModule : SmartSoftwareModule //TODO: Rename to SmartSoftware.SettingManagement.Domain.Tests..?
{

}
