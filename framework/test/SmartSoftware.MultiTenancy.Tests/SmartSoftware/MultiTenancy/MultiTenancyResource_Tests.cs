using Microsoft.Extensions.Localization;
using Shouldly;
using SmartSoftware.Localization;
using SmartSoftware.MultiTenancy.Localization;
using Xunit;

namespace SmartSoftware.MultiTenancy;

public class MultiTenancyResource_Tests : MultiTenancyTestBase
{
    [Fact]
    public void MultiTenancyResource_Test()
    {
        var q = GetRequiredService<IStringLocalizer<SmartSoftwareMultiTenancyResource>>();
        using (CultureHelper.Use("en"))
        {
            GetRequiredService<IStringLocalizer<SmartSoftwareMultiTenancyResource>>()["TenantNotFoundMessage"].Value.ShouldBe("Tenant not found!");
        }

        using (CultureHelper.Use("tr"))
        {
            GetRequiredService<IStringLocalizer<SmartSoftwareMultiTenancyResource>>()["TenantNotFoundMessage"].Value.ShouldBe("Kiracı bulunamadı!");
        }
    }
}
