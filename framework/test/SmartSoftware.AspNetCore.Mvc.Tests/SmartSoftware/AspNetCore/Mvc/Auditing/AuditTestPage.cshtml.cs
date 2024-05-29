using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.Auditing;

namespace SmartSoftware.AspNetCore.Mvc.Auditing;

public class AuditTestPage : SmartSoftwarePageModel
{
    private readonly SmartSoftwareAuditingOptions _options;

    public AuditTestPage(IOptions<SmartSoftwareAuditingOptions> options)
    {
        _options = options.Value;
    }

    public void OnGet()
    {

    }

    public IActionResult OnGetAuditSuccessForGetRequests()
    {
        return new OkResult();
    }

    public IActionResult OnGetAuditFailForGetRequests()
    {
        throw new UserFriendlyException("Exception occurred!");
    }

    public ObjectResult OnGetAuditFailForGetRequestsReturningObject()
    {
        throw new UserFriendlyException("Exception occurred!");
    }

    public IActionResult OnGetAuditActivateFailed([FromServices] SmartSoftwareAuditingOptions options)
    {
        return new OkResult();
    }
}
