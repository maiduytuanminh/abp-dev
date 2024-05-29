using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Permissions;

namespace SmartSoftware.CmsKit.Admin.GlobalResources;

[RequiresFeature(CmsKitFeatures.GlobalResourceEnable)]
[RequiresGlobalFeature(typeof(GlobalResourcesFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitAdminRemoteServiceConsts.ModuleName)]
[Authorize(CmsKitAdminPermissions.Menus.Default)]
[Route("api/cms-kit-admin/global-resources")]
public class GlobalResourceAdminController : CmsKitAdminController, IGlobalResourceAdminAppService
{
    private readonly IGlobalResourceAdminAppService _globalResourceAdminAppService;

    public GlobalResourceAdminController(IGlobalResourceAdminAppService globalResourceAdminAppService)
    {
        _globalResourceAdminAppService = globalResourceAdminAppService;
    }

    [HttpGet]
    public Task<GlobalResourcesDto> GetAsync()
    {
        return _globalResourceAdminAppService.GetAsync();
    }

    [HttpPost]
    public Task SetGlobalResourcesAsync(GlobalResourcesUpdateDto input)
    {
        return _globalResourceAdminAppService.SetGlobalResourcesAsync(input);
    }
}