using SmartSoftware.Identity.Localization;
using SmartSoftware.Identity.Settings;
using SmartSoftware.Localization;
using SmartSoftware.Settings;

namespace SmartSoftware.Identity;

public class SmartSoftwareIdentitySettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                IdentitySettingNames.Password.RequiredLength,
                6.ToString(),
                L("DisplayName:SmartSoftware.Identity.Password.RequiredLength"),
                L("Description:SmartSoftware.Identity.Password.RequiredLength"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequiredUniqueChars,
                1.ToString(),
                L("DisplayName:SmartSoftware.Identity.Password.RequiredUniqueChars"),
                L("Description:SmartSoftware.Identity.Password.RequiredUniqueChars"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequireNonAlphanumeric,
                true.ToString(),
                L("DisplayName:SmartSoftware.Identity.Password.RequireNonAlphanumeric"),
                L("Description:SmartSoftware.Identity.Password.RequireNonAlphanumeric"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequireLowercase,
                true.ToString(),
                L("DisplayName:SmartSoftware.Identity.Password.RequireLowercase"),
                L("Description:SmartSoftware.Identity.Password.RequireLowercase"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequireUppercase,
                true.ToString(),
                L("DisplayName:SmartSoftware.Identity.Password.RequireUppercase"),
                L("Description:SmartSoftware.Identity.Password.RequireUppercase"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequireDigit,
                true.ToString(),
                L("DisplayName:SmartSoftware.Identity.Password.RequireDigit"),
                L("Description:SmartSoftware.Identity.Password.RequireDigit"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.ForceUsersToPeriodicallyChangePassword,
                false.ToString(),
                L("DisplayName:SmartSoftware.Identity.Password.ForceUsersToPeriodicallyChangePassword"),
                L("Description:SmartSoftware.Identity.Password.ForceUsersToPeriodicallyChangePassword"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.PasswordChangePeriodDays,
                0.ToString(),
                L("DisplayName:SmartSoftware.Identity.Password.PasswordChangePeriodDays"),
                L("Description:SmartSoftware.Identity.Password.PasswordChangePeriodDays"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Lockout.AllowedForNewUsers,
                true.ToString(),
                L("DisplayName:SmartSoftware.Identity.Lockout.AllowedForNewUsers"),
                L("Description:SmartSoftware.Identity.Lockout.AllowedForNewUsers"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Lockout.LockoutDuration,
                (5 * 60).ToString(),
                L("DisplayName:SmartSoftware.Identity.Lockout.LockoutDuration"),
                L("Description:SmartSoftware.Identity.Lockout.LockoutDuration"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Lockout.MaxFailedAccessAttempts,
                5.ToString(),
                L("DisplayName:SmartSoftware.Identity.Lockout.MaxFailedAccessAttempts"),
                L("Description:SmartSoftware.Identity.Lockout.MaxFailedAccessAttempts"),
                true),

            new SettingDefinition(
                IdentitySettingNames.SignIn.RequireConfirmedEmail,
                false.ToString(),
                L("DisplayName:SmartSoftware.Identity.SignIn.RequireConfirmedEmail"),
                L("Description:SmartSoftware.Identity.SignIn.RequireConfirmedEmail"),
                true),
            new SettingDefinition(
                IdentitySettingNames.SignIn.EnablePhoneNumberConfirmation,
                true.ToString(),
                L("DisplayName:SmartSoftware.Identity.SignIn.EnablePhoneNumberConfirmation"),
                L("Description:SmartSoftware.Identity.SignIn.EnablePhoneNumberConfirmation"),
                true),
            new SettingDefinition(
                IdentitySettingNames.SignIn.RequireConfirmedPhoneNumber,
                false.ToString(),
                L("DisplayName:SmartSoftware.Identity.SignIn.RequireConfirmedPhoneNumber"),
                L("Description:SmartSoftware.Identity.SignIn.RequireConfirmedPhoneNumber"),
                true),

            new SettingDefinition(
                IdentitySettingNames.User.IsUserNameUpdateEnabled,
                true.ToString(),
                L("DisplayName:SmartSoftware.Identity.User.IsUserNameUpdateEnabled"),
                L("Description:SmartSoftware.Identity.User.IsUserNameUpdateEnabled"),
                true),

            new SettingDefinition(
                IdentitySettingNames.User.IsEmailUpdateEnabled,
                true.ToString(),
                L("DisplayName:SmartSoftware.Identity.User.IsEmailUpdateEnabled"),
                L("Description:SmartSoftware.Identity.User.IsEmailUpdateEnabled"),
                true),

            new SettingDefinition(
                IdentitySettingNames.OrganizationUnit.MaxUserMembershipCount,
                int.MaxValue.ToString(),
                L("Identity.OrganizationUnit.MaxUserMembershipCount"),
                L("Identity.OrganizationUnit.MaxUserMembershipCount"),
                true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IdentityResource>(name);
    }
}
