using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.Application.Dtos;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Users;

namespace SmartSoftware.Identity;

[RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
[Area(IdentityRemoteServiceConsts.ModuleName)]
[ControllerName("UserLookup")]
[Route("api/identity/users/lookup")]
public class IdentityUserLookupController : SmartSoftwareControllerBase, IIdentityUserLookupAppService
{
    protected IIdentityUserLookupAppService LookupAppService { get; }

    public IdentityUserLookupController(IIdentityUserLookupAppService lookupAppService)
    {
        LookupAppService = lookupAppService;
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<UserData> FindByIdAsync(Guid id)
    {
        return LookupAppService.FindByIdAsync(id);
    }

    [HttpGet]
    [Route("by-username/{userName}")]
    public virtual Task<UserData> FindByUserNameAsync(string userName)
    {
        return LookupAppService.FindByUserNameAsync(userName);
    }

    [HttpGet]
    [Route("search")]
    public Task<ListResultDto<UserData>> SearchAsync(UserLookupSearchInputDto input)
    {
        return LookupAppService.SearchAsync(input);
    }

    [HttpGet]
    [Route("count")]
    public Task<long> GetCountAsync(UserLookupCountInputDto input)
    {
        return LookupAppService.GetCountAsync(input);
    }
}
