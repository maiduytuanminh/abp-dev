using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.AntiForgery;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

[Area("ss")]
[RemoteService(Name = "ss")]
[Route("api/ss/application-configuration")]
public class SmartSoftwareApplicationConfigurationController : SmartSoftwareControllerBase, ISmartSoftwareApplicationConfigurationAppService
{
    protected readonly ISmartSoftwareApplicationConfigurationAppService ApplicationConfigurationAppService;
    protected readonly ISmartSoftwareAntiForgeryManager AntiForgeryManager;

    public SmartSoftwareApplicationConfigurationController(
        ISmartSoftwareApplicationConfigurationAppService applicationConfigurationAppService,
        ISmartSoftwareAntiForgeryManager antiForgeryManager)
    {
        ApplicationConfigurationAppService = applicationConfigurationAppService;
        AntiForgeryManager = antiForgeryManager;
    }

    [HttpGet]
    public virtual async Task<ApplicationConfigurationDto> GetAsync(
        ApplicationConfigurationRequestOptions options)
    {
        AntiForgeryManager.SetCookie();
        return await ApplicationConfigurationAppService.GetAsync(options);
    }
}
