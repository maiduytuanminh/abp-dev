using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Public.GlobalResources;

public interface IGlobalResourcePublicAppService : IApplicationService
{
    Task<GlobalResourceDto> GetGlobalScriptAsync();
    
    Task<GlobalResourceDto> GetGlobalStyleAsync();
}