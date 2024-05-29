using System;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.AspNetCore.Mvc.MultiTenancy;

public interface ISmartSoftwareTenantAppService : IApplicationService
{
    Task<FindTenantResultDto> FindTenantByNameAsync(string name);

    Task<FindTenantResultDto> FindTenantByIdAsync(Guid id);
}
