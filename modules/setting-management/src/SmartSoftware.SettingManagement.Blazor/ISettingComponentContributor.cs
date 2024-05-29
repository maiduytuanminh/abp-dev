using System.Threading.Tasks;

namespace SmartSoftware.SettingManagement.Blazor;

public interface ISettingComponentContributor
{
    Task ConfigureAsync(SettingComponentCreationContext context);

    Task<bool> CheckPermissionsAsync(SettingComponentCreationContext context);
}
