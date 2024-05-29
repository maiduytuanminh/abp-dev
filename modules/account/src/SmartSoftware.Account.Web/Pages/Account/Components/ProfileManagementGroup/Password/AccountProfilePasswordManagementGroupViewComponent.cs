using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;
using SmartSoftware.Auditing;
using SmartSoftware.Identity;
using SmartSoftware.Validation;

namespace SmartSoftware.Account.Web.Pages.Account.Components.ProfileManagementGroup.Password;

public class AccountProfilePasswordManagementGroupViewComponent : SmartSoftwareViewComponent
{
    protected IProfileAppService ProfileAppService { get; }

    public AccountProfilePasswordManagementGroupViewComponent(
        IProfileAppService profileAppService)
    {
        ProfileAppService = profileAppService;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await ProfileAppService.GetAsync();

        var model = new ChangePasswordInfoModel
        {
            HideOldPasswordInput = !user.HasPassword
        };

        return View("~/Pages/Account/Components/ProfileManagementGroup/Password/Default.cshtml", model);
    }

    public class ChangePasswordInfoModel
    {
        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        [Display(Name = "DisplayName:CurrentPassword")]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string CurrentPassword { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        [Display(Name = "DisplayName:NewPassword")]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string NewPassword { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
        [Display(Name = "DisplayName:NewPasswordConfirm")]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string NewPasswordConfirm { get; set; }

        public bool HideOldPasswordInput { get; set; }
    }
}
