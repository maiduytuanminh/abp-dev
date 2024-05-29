using System;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Ldap;
/// <summary>
/// docker run --name ldap -d --env LDAP_ORGANISATION="ss" --env LDAP_DOMAIN="smartsoftware.io" --env LDAP_ADMIN_PASSWORD="123qwe" -p 389:389 -p 636:639 osixia/openldap
/// </summary>
public class LdapManager_Tests : SmartSoftwareIntegratedTest<SmartSoftwareLdapTestModule>
{
    private readonly ILdapManager _ldapManager;

    public LdapManager_Tests()
    {
        _ldapManager = GetRequiredService<ILdapManager>();
    }

    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact(Skip = "Required Ldap environment")]
    public async Task AuthenticateAsync()
    {
        (await _ldapManager.AuthenticateAsync("cn=admin,dc=ss,dc=io", "123qwe")).ShouldBe(true);
        (await _ldapManager.AuthenticateAsync("cn=ss,dc=ss,dc=io", "123123")).ShouldBe(false);
        (await _ldapManager.AuthenticateAsync("NoExists", "123qwe")).ShouldBe(false);
    }
}
