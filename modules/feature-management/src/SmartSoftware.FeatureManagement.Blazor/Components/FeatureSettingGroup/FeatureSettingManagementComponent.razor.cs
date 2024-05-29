using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using SmartSoftware.AspNetCore.Components;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.FeatureManagement.Localization;
using SmartSoftware.Features;

namespace SmartSoftware.FeatureManagement.Blazor.Components.FeatureSettingGroup;

public partial class FeatureSettingManagementComponent : SmartSoftwareComponentBase
{
    [Inject]
    protected PermissionChecker PermissionChecker { get; set; }
    
    protected FeatureManagementModal FeatureManagementModal;
    
    protected FeatureSettingViewModel Settings;

    public FeatureSettingManagementComponent()
    {
        LocalizationResource = typeof(SmartSoftwareFeatureManagementResource);
    }

    protected async override Task OnInitializedAsync()
    {
        Settings = new FeatureSettingViewModel 
        {
            HasManageHostFeaturesPermission = await AuthorizationService.IsGrantedAsync(FeatureManagementPermissions.ManageHostFeatures)
        };
    }

    protected virtual async Task OnManageHostFeaturesClicked()
    {
       await FeatureManagementModal.OpenAsync(TenantFeatureValueProvider.ProviderName);
    }
}