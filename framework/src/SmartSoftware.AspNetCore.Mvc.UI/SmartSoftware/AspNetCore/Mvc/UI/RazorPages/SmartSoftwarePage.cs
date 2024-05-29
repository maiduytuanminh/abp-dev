using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartSoftware.Users;

namespace SmartSoftware.AspNetCore.Mvc.UI.RazorPages;

public abstract class SmartSoftwarePage : Page
{
    [RazorInject]
    public ICurrentUser CurrentUser { get; set; } = default!;
}
