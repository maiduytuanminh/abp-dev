using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Admin.GlobalResources;

public interface IGlobalResourceAdminAppService : IApplicationService
{
    Task<GlobalResourcesDto> GetAsync();

    Task SetGlobalResourcesAsync(GlobalResourcesUpdateDto input);
}