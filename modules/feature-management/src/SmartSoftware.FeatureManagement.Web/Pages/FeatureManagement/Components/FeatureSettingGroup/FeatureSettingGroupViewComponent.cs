using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Features;

namespace SmartSoftware.FeatureManagement.Pages.FeatureManagement.Components.FeatureSettingGroup;

public class FeatureSettingGroupViewComponent: SmartSoftwareViewComponent
{
    protected IPermissionChecker PermissionChecker { get; }
    
    public FeatureSettingGroupViewComponent(IPermissionChecker permissionChecker)
    {
        PermissionChecker = permissionChecker;
    }
    
    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new FeatureSettingViewModel
        {
            HasManageHostFeaturesPermission = await PermissionChecker.IsGrantedAsync(FeatureManagementPermissions.ManageHostFeatures)
        };
        return View("~/Pages/FeatureManagement/Components/FeatureSettingGroup/Default.cshtml", model);
    }
    
    public class FeatureSettingViewModel
    {
        public bool HasManageHostFeaturesPermission { get; set; }
    }
}