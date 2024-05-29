using System;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Localization;
using SmartSoftware.Account.Emailing.Templates;
using SmartSoftware.Account.Localization;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Emailing;
using SmartSoftware.Identity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.TextTemplating;
using SmartSoftware.UI.Navigation.Urls;

namespace SmartSoftware.Account.Emailing;

public class AccountEmailer : IAccountEmailer, ITransientDependency
{
    protected ITemplateRenderer TemplateRenderer { get; }
    protected IEmailSender EmailSender { get; }
    protected IStringLocalizer<AccountResource> StringLocalizer { get; }
    protected IAppUrlProvider AppUrlProvider { get; }
    protected ICurrentTenant CurrentTenant { get; }

    public AccountEmailer(
        IEmailSender emailSender,
        ITemplateRenderer templateRenderer,
        IStringLocalizer<AccountResource> stringLocalizer,
        IAppUrlProvider appUrlProvider,
        ICurrentTenant currentTenant)
    {
        EmailSender = emailSender;
        StringLocalizer = stringLocalizer;
        AppUrlProvider = appUrlProvider;
        CurrentTenant = currentTenant;
        TemplateRenderer = templateRenderer;
    }

    public virtual async Task SendPasswordResetLinkAsync(
        IdentityUser user,
        string resetToken,
        string appName,
        string returnUrl = null,
        string returnUrlHash = null)
    {
        Debug.Assert(CurrentTenant.Id == user.TenantId, "This method can only work for current tenant!");

        var url = await AppUrlProvider.GetResetPasswordUrlAsync(appName);

        //TODO: Use SmartSoftwareAspNetCoreMultiTenancyOptions to get the key
        var link = $"{url}?userId={user.Id}&{TenantResolverConsts.DefaultTenantKey}={user.TenantId}&resetToken={UrlEncoder.Default.Encode(resetToken)}";

        if (!returnUrl.IsNullOrEmpty())
        {
            link += "&returnUrl=" + NormalizeReturnUrl(returnUrl);
        }

        if (!returnUrlHash.IsNullOrEmpty())
        {
            link += "&returnUrlHash=" + returnUrlHash;
        }

        var emailContent = await TemplateRenderer.RenderAsync(
            AccountEmailTemplates.PasswordResetLink,
            new { link = link }
        );

        await EmailSender.SendAsync(
            user.Email,
            StringLocalizer["PasswordReset"],
            emailContent
        );
    }

    protected virtual string NormalizeReturnUrl(string returnUrl)
    {
        if (returnUrl.IsNullOrEmpty())
        {
            return returnUrl;
        }

        //Handling openid connect login
        if (returnUrl.StartsWith("/connect/authorize/callback", StringComparison.OrdinalIgnoreCase))
        {
            if (returnUrl.Contains("?"))
            {
                var queryPart = returnUrl.Split('?')[1];
                var queryParameters = queryPart.Split('&');
                foreach (var queryParameter in queryParameters)
                {
                    if (queryParameter.Contains("="))
                    {
                        var queryParam = queryParameter.Split('=');
                        if (queryParam[0] == "redirect_uri")
                        {
                            return HttpUtility.UrlDecode(queryParam[1]);
                        }
                    }
                }
            }
        }

        if (returnUrl.StartsWith("/connect/authorize?", StringComparison.OrdinalIgnoreCase))
        {
            return HttpUtility.UrlEncode(returnUrl);
        }

        return returnUrl;
    }
}
