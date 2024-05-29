using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;

namespace SmartSoftware.TenantManagement.Web.Pages.TenantManagement.Tenants;

public abstract class TenantManagementPageModel : SmartSoftwarePageModel
{
    public TenantManagementPageModel()
    {
        ObjectMapperContext = typeof(SmartSoftwareTenantManagementWebModule);
    }
}
