﻿using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.Application.Dtos;
using SmartSoftware.AspNetCore.Mvc;

namespace SmartSoftware.Identity;

[RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
[Area(IdentityRemoteServiceConsts.ModuleName)]
[ControllerName("Role")]
[Route("api/identity/roles")]
public class IdentityRoleController : SmartSoftwareControllerBase, IIdentityRoleAppService
{
    protected IIdentityRoleAppService RoleAppService { get; }

    public IdentityRoleController(IIdentityRoleAppService roleAppService)
    {
        RoleAppService = roleAppService;
    }

    [HttpGet]
    [Route("all")]
    public virtual Task<ListResultDto<IdentityRoleDto>> GetAllListAsync()
    {
        return RoleAppService.GetAllListAsync();
    }

    [HttpGet]
    public virtual Task<PagedResultDto<IdentityRoleDto>> GetListAsync(GetIdentityRolesInput input)
    {
        return RoleAppService.GetListAsync(input);
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<IdentityRoleDto> GetAsync(Guid id)
    {
        return RoleAppService.GetAsync(id);
    }

    [HttpPost]
    public virtual Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
    {
        return RoleAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<IdentityRoleDto> UpdateAsync(Guid id, IdentityRoleUpdateDto input)
    {
        return RoleAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return RoleAppService.DeleteAsync(id);
    }
}
