using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SmartSoftware.Account.Localization;
using SmartSoftware.AspNetCore.ExceptionHandling;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Identity;
using IdentityUser = SmartSoftware.Identity.IdentityUser;

namespace SmartSoftware.Account.Web.Pages.Account;

public abstract class AccountPageModel : SmartSoftwarePageModel
{
    public IAccountAppService AccountAppService { get; set; }
    public SignInManager<IdentityUser> SignInManager { get; set; }
    public IdentityUserManager UserManager { get; set; }
    public IdentitySecurityLogManager IdentitySecurityLogManager { get; set; }
    public IOptions<IdentityOptions> IdentityOptions { get; set; }
    public IExceptionToErrorInfoConverter ExceptionToErrorInfoConverter { get; set; }

    protected AccountPageModel()
    {
        LocalizationResourceType = typeof(AccountResource);
        ObjectMapperContext = typeof(SmartSoftwareAccountWebModule);
    }

    protected virtual void CheckCurrentTenant(Guid? tenantId)
    {
        if (CurrentTenant.Id != tenantId)
        {
            throw new ApplicationException($"Current tenant is different than given tenant. CurrentTenant.Id: {CurrentTenant.Id}, given tenantId: {tenantId}");
        }
    }

    protected virtual void CheckIdentityErrors(IdentityResult identityResult)
    {
        if (!identityResult.Succeeded)
        {
            throw new UserFriendlyException("Operation failed: " + identityResult.Errors.Select(e => $"[{e.Code}] {e.Description}").JoinAsString(", "));
        }

        //identityResult.CheckErrors(LocalizationManager); //TODO: Get from old SmartSoftware
    }

    protected virtual string GetLocalizeExceptionMessage(Exception exception)
    {
        if (exception is ILocalizeErrorMessage || exception is IHasErrorCode)
        {
            return ExceptionToErrorInfoConverter.Convert(exception, false).Message;
        }

        return exception.Message;
    }
}
