using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace SmartSoftware.CmsKit.Pages;

public class IndexModel : CmsKitPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
