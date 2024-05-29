using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;

namespace SmartSoftware.PermissionManagement;

[RemoteService(Name = PermissionManagementRemoteServiceConsts.RemoteServiceName)]
[Area(PermissionManagementRemoteServiceConsts.ModuleName)]
[Route("api/permission-management/permissions")]
public class PermissionsController : SmartSoftwareControllerBase, IPermissionAppService
{
    protected IPermissionAppService PermissionAppService { get; }

    public PermissionsController(IPermissionAppService permissionAppService)
    {
        PermissionAppService = permissionAppService;
    }

    [HttpGet]
    public virtual Task<GetPermissionListResultDto> GetAsync(string providerName, string providerKey)
    {
        return PermissionAppService.GetAsync(providerName, providerKey);
    }

    [HttpPut]
    public virtual Task UpdateAsync(string providerName, string providerKey, UpdatePermissionsDto input)
    {
        return PermissionAppService.UpdateAsync(providerName, providerKey, input);
    }
}
