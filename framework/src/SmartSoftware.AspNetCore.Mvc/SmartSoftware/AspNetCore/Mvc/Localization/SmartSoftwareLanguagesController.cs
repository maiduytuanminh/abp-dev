using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.RequestLocalization;
using SmartSoftware.Auditing;
using SmartSoftware.Localization;

namespace SmartSoftware.AspNetCore.Mvc.Localization;

[Area("SmartSoftware")]
[Route("SmartSoftware/Languages/[action]")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class SmartSoftwareLanguagesController : SmartSoftwareController
{
    protected IQueryStringCultureReplacement QueryStringCultureReplacement { get; }

    public SmartSoftwareLanguagesController(IQueryStringCultureReplacement queryStringCultureReplacement)
    {
        QueryStringCultureReplacement = queryStringCultureReplacement;
    }

    [HttpGet]
    public virtual async Task<IActionResult> Switch(string culture, string uiCulture = "", string returnUrl = "")
    {
        if (!CultureHelper.IsValidCultureCode(culture))
        {
            throw new SmartSoftwareException("The selected culture is not valid! Make sure you enter a valid culture code.");
        }

        if (!CultureHelper.IsValidCultureCode(uiCulture))
        {
            throw new SmartSoftwareException("The selected uiCulture is not valid! Make sure you enter a valid culture code.");
        }

        SmartSoftwareRequestCultureCookieHelper.SetCultureCookie(
            HttpContext,
            new RequestCulture(culture, uiCulture)
        );

        HttpContext.Items[SmartSoftwareRequestLocalizationMiddleware.HttpContextItemName] = true;

        var context = new QueryStringCultureReplacementContext(HttpContext, new RequestCulture(culture, uiCulture), returnUrl);
        await QueryStringCultureReplacement.ReplaceAsync(context);

        if (!string.IsNullOrWhiteSpace(context.ReturnUrl))
        {
            return Redirect(GetRedirectUrl(context.ReturnUrl));
        }

        return Redirect("~/");
    }

    protected virtual string GetRedirectUrl(string returnUrl)
    {
        if (returnUrl.IsNullOrEmpty())
        {
            return "~/";
        }

        if (Url.IsLocalUrl(returnUrl))
        {
            return returnUrl;
        }

        return "~/";
    }
}
