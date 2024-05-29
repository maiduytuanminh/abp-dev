using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public interface ISmartSoftwareApplicationConfigurationAppService : IApplicationService
{
    Task<ApplicationConfigurationDto> GetAsync(ApplicationConfigurationRequestOptions options);
}