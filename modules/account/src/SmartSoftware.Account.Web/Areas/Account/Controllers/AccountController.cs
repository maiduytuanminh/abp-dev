using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.Account.Localization;
using SmartSoftware.Account.Settings;
using SmartSoftware.Account.Web.Areas.Account.Controllers.Models;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Identity;
using SmartSoftware.Identity.AspNetCore;
using SmartSoftware.Settings;
using SmartSoftware.Validation;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using UserLoginInfo = SmartSoftware.Account.Web.Areas.Account.Controllers.Models.UserLoginInfo;
using IdentityUser = SmartSoftware.Identity.IdentityUser;

namespace SmartSoftware.Account.Web.Areas.Account.Controllers;

[RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
[Controller]
[ControllerName("Login")]
[Area("account")]
[Route("api/account")]
public class AccountController : SmartSoftwareControllerBase
{
    protected SignInManager<IdentityUser> SignInManager { get; }
    protected IdentityUserManager UserManager { get; }
    protected ISettingProvider SettingProvider { get; }
    protected IdentitySecurityLogManager IdentitySecurityLogManager { get; }
    protected IOptions<IdentityOptions> IdentityOptions { get; }
    protected IdentityDynamicClaimsPrincipalContributorCache IdentityDynamicClaimsPrincipalContributorCache { get; }

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        IdentityUserManager userManager,
        ISettingProvider settingProvider,
        IdentitySecurityLogManager identitySecurityLogManager,
        IOptions<IdentityOptions> identityOptions,
        IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache)
    {
        LocalizationResource = typeof(AccountResource);

        SignInManager = signInManager;
        UserManager = userManager;
        SettingProvider = settingProvider;
        IdentitySecurityLogManager = identitySecurityLogManager;
        IdentityOptions = identityOptions;
        IdentityDynamicClaimsPrincipalContributorCache = identityDynamicClaimsPrincipalContributorCache;
    }

    [HttpPost]
    [Route("login")]
    public virtual async Task<SmartSoftwareLoginResult> Login(UserLoginInfo login)
    {
        await CheckLocalLoginAsync();

        ValidateLoginInfo(login);

        await ReplaceEmailToUsernameOfInputIfNeeds(login);

        await IdentityOptions.SetAsync();

        var signInResult = await SignInManager.PasswordSignInAsync(
            login.UserNameOrEmailAddress,
            login.Password,
            login.RememberMe,
            true
        );

        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
        {
            Identity = IdentitySecurityLogIdentityConsts.Identity,
            Action = signInResult.ToIdentitySecurityLogAction(),
            UserName = login.UserNameOrEmailAddress
        });

        if (signInResult.Succeeded)
        {
            var user = await UserManager.FindByNameAsync(login.UserNameOrEmailAddress);
            if (user != null)
            {
                // Clear the dynamic claims cache.
                await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);
            }
        }

        return GetSmartSoftwareLoginResult(signInResult);
    }

    [HttpGet]
    [Route("logout")]
    public virtual async Task Logout()
    {
        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
        {
            Identity = IdentitySecurityLogIdentityConsts.Identity,
            Action = IdentitySecurityLogActionConsts.Logout
        });

        await SignInManager.SignOutAsync();
    }

    [HttpPost]
    [Route("checkPassword")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public virtual Task<SmartSoftwareLoginResult> CheckPasswordCompatible(UserLoginInfo login)
    {
        return CheckPassword(login);
    }

    [HttpPost]
    [Route("check-password")]
    public virtual async Task<SmartSoftwareLoginResult> CheckPassword(UserLoginInfo login)
    {
        ValidateLoginInfo(login);

        await ReplaceEmailToUsernameOfInputIfNeeds(login);

        var identityUser = await UserManager.FindByNameAsync(login.UserNameOrEmailAddress);

        if (identityUser == null)
        {
            return new SmartSoftwareLoginResult(LoginResultType.InvalidUserNameOrPassword);
        }

        await IdentityOptions.SetAsync();
        return GetSmartSoftwareLoginResult(await SignInManager.CheckPasswordSignInAsync(identityUser, login.Password, true));
    }

    protected virtual async Task ReplaceEmailToUsernameOfInputIfNeeds(UserLoginInfo login)
    {
        if (!ValidationHelper.IsValidEmailAddress(login.UserNameOrEmailAddress))
        {
            return;
        }

        var userByUsername = await UserManager.FindByNameAsync(login.UserNameOrEmailAddress);
        if (userByUsername != null)
        {
            return;
        }

        var userByEmail = await UserManager.FindByEmailAsync(login.UserNameOrEmailAddress);
        if (userByEmail == null)
        {
            return;
        }

        login.UserNameOrEmailAddress = userByEmail.UserName;
    }

    private static SmartSoftwareLoginResult GetSmartSoftwareLoginResult(SignInResult result)
    {
        if (result.IsLockedOut)
        {
            return new SmartSoftwareLoginResult(LoginResultType.LockedOut);
        }

        if (result.RequiresTwoFactor)
        {
            return new SmartSoftwareLoginResult(LoginResultType.RequiresTwoFactor);
        }

        if (result.IsNotAllowed)
        {
            return new SmartSoftwareLoginResult(LoginResultType.NotAllowed);
        }

        if (!result.Succeeded)
        {
            return new SmartSoftwareLoginResult(LoginResultType.InvalidUserNameOrPassword);
        }

        return new SmartSoftwareLoginResult(LoginResultType.Success);
    }

    protected virtual void ValidateLoginInfo(UserLoginInfo login)
    {
        if (login == null)
        {
            throw new ArgumentException(nameof(login));
        }

        if (login.UserNameOrEmailAddress.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(login.UserNameOrEmailAddress));
        }

        if (login.Password.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(login.Password));
        }
    }

    protected virtual async Task CheckLocalLoginAsync()
    {
        if (!await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
        {
            throw new UserFriendlyException(L["LocalLoginDisabledMessage"]);
        }
    }
}
