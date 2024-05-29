using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartSoftware.Application.Services;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.GlobalResources;
using SmartSoftware.CmsKit.Permissions;

namespace SmartSoftware.CmsKit.Admin.GlobalResources;

[RequiresFeature(CmsKitFeatures.GlobalResourceEnable)]
[RequiresGlobalFeature(typeof(GlobalResourcesFeature))]
[Authorize(CmsKitAdminPermissions.GlobalResources.Default)]
public class GlobalResourceAdminAppService : ApplicationService, IGlobalResourceAdminAppService
{
    public GlobalResourceManager GlobalResourceManager { get; }

    public GlobalResourceAdminAppService(GlobalResourceManager globalResourceManager)
    {
        GlobalResourceManager = globalResourceManager;
    }

    public virtual async Task<GlobalResourcesDto> GetAsync()
    {
        return new GlobalResourcesDto
        {
            StyleContent = (await GlobalResourceManager.GetGlobalStyleAsync()).Value,
            ScriptContent = (await GlobalResourceManager.GetGlobalScriptAsync()).Value
        };
    }

    public virtual async Task SetGlobalResourcesAsync(GlobalResourcesUpdateDto input)
    {
        await GlobalResourceManager.SetGlobalStyleAsync(input.Style);
        await GlobalResourceManager.SetGlobalScriptAsync(input.Script);
    }
}