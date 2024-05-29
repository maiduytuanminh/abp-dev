using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;

namespace SmartSoftware.SettingManagement;

[RemoteService(Name = SettingManagementRemoteServiceConsts.RemoteServiceName)]
[Area(SettingManagementRemoteServiceConsts.ModuleName)]
[Route("api/setting-management/emailing")]
public class EmailSettingsController : SmartSoftwareControllerBase, IEmailSettingsAppService
{
    private readonly IEmailSettingsAppService _emailSettingsAppService;

    public EmailSettingsController(IEmailSettingsAppService emailSettingsAppService)
    {
        _emailSettingsAppService = emailSettingsAppService;
    }

    [HttpGet]
    public Task<EmailSettingsDto> GetAsync()
    {
        return _emailSettingsAppService.GetAsync();
    }

    [HttpPost]
    public Task UpdateAsync(UpdateEmailSettingsDto input)
    {
        return _emailSettingsAppService.UpdateAsync(input);
    }

    [HttpPost("send-test-email")]
    public Task SendTestEmailAsync(SendTestEmailInput input)
    {
        return _emailSettingsAppService.SendTestEmailAsync(input);
    }
}
