using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public interface ISmartSoftwareApplicationLocalizationAppService : IApplicationService
{
    Task<ApplicationLocalizationDto> GetAsync(ApplicationLocalizationRequestDto input);
}