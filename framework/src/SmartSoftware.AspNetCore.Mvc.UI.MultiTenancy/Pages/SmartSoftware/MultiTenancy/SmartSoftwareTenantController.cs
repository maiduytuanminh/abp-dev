using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.AspNetCore;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.MultiTenancy;

namespace Pages.SmartSoftware.MultiTenancy;

[Area("ss")]
[RemoteService(Name = "ss")]
[Route("api/ss/multi-tenancy")]
public class SmartSoftwareTenantController : SmartSoftwareControllerBase, ISmartSoftwareTenantAppService
{
    private readonly ISmartSoftwareTenantAppService _ssTenantAppService;

    public SmartSoftwareTenantController(ISmartSoftwareTenantAppService ssTenantAppService)
    {
        _ssTenantAppService = ssTenantAppService;
    }

    [HttpGet]
    [Route("tenants/by-name/{name}")]
    public virtual async Task<FindTenantResultDto> FindTenantByNameAsync(string name)
    {
        return await _ssTenantAppService.FindTenantByNameAsync(name);
    }

    [HttpGet]
    [Route("tenants/by-id/{id}")]
    public virtual async Task<FindTenantResultDto> FindTenantByIdAsync(Guid id)
    {
        return await _ssTenantAppService.FindTenantByIdAsync(id);
    }
}
