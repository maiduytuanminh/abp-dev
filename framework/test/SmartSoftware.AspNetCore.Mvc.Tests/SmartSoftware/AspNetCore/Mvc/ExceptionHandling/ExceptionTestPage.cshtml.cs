using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.Authorization;

namespace SmartSoftware.AspNetCore.Mvc.ExceptionHandling;

public class ExceptionTestPage : SmartSoftwarePageModel
{
    public void OnGetUserFriendlyException_void()
    {
        throw new UserFriendlyException("This is a sample exception!");
    }

    public Task OnGetUserFriendlyException_Task()
    {
        throw new UserFriendlyException("This is a sample exception!");
    }

    public IActionResult OnGetUserFriendlyException_ActionResult()
    {
        throw new UserFriendlyException("This is a sample exception!");
    }

    public JsonResult OnGetUserFriendlyException_JsonResult()
    {
        throw new UserFriendlyException("This is a sample exception!");
    }

    public Task<JsonResult> OnGetUserFriendlyException_Task_JsonResult()
    {
        throw new UserFriendlyException("This is a sample exception!");
    }

    public Task<JsonResult> OnGetSmartSoftwareAuthorizationException()
    {
        throw new SmartSoftwareAuthorizationException("This is a sample exception!");
    }

    public Task<JsonResult> OnGetExceptionOnUowSaveChangeAsync()
    {
        return Task.FromResult(new JsonResult("OK"));
    }
}
