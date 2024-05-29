using System.Globalization;
using Microsoft.Extensions.Localization;
using Shouldly;
using SmartSoftware.Localization;
using SmartSoftware.TenantManagement.Localization;
using Xunit;

namespace SmartSoftware.TenantManagement;

public class Localization_Tests : SmartSoftwareTenantManagementDomainTestBase
{
    private readonly IStringLocalizer<SmartSoftwareTenantManagementResource> _stringLocalizer;

    public Localization_Tests()
    {
        _stringLocalizer = GetRequiredService<IStringLocalizer<SmartSoftwareTenantManagementResource>>();
    }

    [Fact]
    public void Test()
    {
        using (CultureHelper.Use("en"))
        {
            _stringLocalizer["TenantDeletionConfirmationMessage"].Value
                .ShouldBe("Tenant '{0}' will be deleted. Do you confirm that?");
        }

        using (CultureHelper.Use("en-gb"))
        {
            _stringLocalizer["TenantDeletionConfirmationMessage"].Value
                .ShouldBe("Tenant '{0}' will be deleted. Is that OK?");
        }

        using (CultureHelper.Use("tr"))
        {
            _stringLocalizer["TenantDeletionConfirmationMessage"].Value
                .ShouldBe("'{0}' isimli müşteri silinecektir. Onaylıyor musunuz?");
        }
    }
}
