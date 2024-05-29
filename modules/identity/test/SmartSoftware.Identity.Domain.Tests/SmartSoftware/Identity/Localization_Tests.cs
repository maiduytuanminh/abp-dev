using Microsoft.Extensions.Localization;
using Shouldly;
using SmartSoftware.Identity;
using SmartSoftware.Identity.Localization;
using SmartSoftware.Localization;
using Xunit;

namespace SmartSoftware.TenantManagement;

public class Localization_Tests : SmartSoftwareIdentityDomainTestBase
{
    private readonly IStringLocalizer<IdentityResource> _stringLocalizer;

    public Localization_Tests()
    {
        _stringLocalizer = GetRequiredService<IStringLocalizer<IdentityResource>>();
    }

    [Fact]
    public void Test()
    {
        using (CultureHelper.Use("en"))
        {
            _stringLocalizer["PersonalSettingsSavedMessage"].Value
            .ShouldBe("Your personal settings has been saved successfully.");
        }
    }
}
