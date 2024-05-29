using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;

namespace SmartSoftware.AspNetCore.Mvc.Authorization;

[Authorize]
public class AuthTestPage : SmartSoftwarePageModel
{
    public static Guid FakeUserId { get; } = Guid.NewGuid();

    public ActionResult OnGet()
    {
        return Content("OK");
    }
}
