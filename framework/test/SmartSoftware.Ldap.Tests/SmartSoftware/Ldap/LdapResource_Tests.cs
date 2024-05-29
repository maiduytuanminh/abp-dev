using Microsoft.Extensions.Localization;
using Shouldly;
using SmartSoftware.Ldap.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Ldap;

public class LdapResource_Tests : SmartSoftwareIntegratedTest<SmartSoftwareLdapTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact]
    public void LdapResource_Test()
    {
        using (CultureHelper.Use("en"))
        {
            GetRequiredService<IStringLocalizer<LdapResource>>()["DisplayName:SmartSoftware.Ldap.ServerHost"].Value.ShouldBe("Server host");
        }

        using (CultureHelper.Use("tr"))
        {
            GetRequiredService<IStringLocalizer<LdapResource>>()["DisplayName:SmartSoftware.Ldap.ServerHost"].Value.ShouldBe("Sunucu Ana Bilgisayarı");
        }
    }
}
