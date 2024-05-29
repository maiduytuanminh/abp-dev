using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

[Area("ss")]
[RemoteService(Name = "ss")]
[Route("api/ss/application-localization")]
public class SmartSoftwareApplicationLocalizationController: SmartSoftwareControllerBase, ISmartSoftwareApplicationLocalizationAppService
{
    private readonly ISmartSoftwareApplicationLocalizationAppService _localizationAppService;

    public SmartSoftwareApplicationLocalizationController(ISmartSoftwareApplicationLocalizationAppService localizationAppService)
    {
        _localizationAppService = localizationAppService;
    }
    
    [HttpGet]
    public virtual async Task<ApplicationLocalizationDto> GetAsync(ApplicationLocalizationRequestDto input)
    {
        return await _localizationAppService.GetAsync(input);
    }
}