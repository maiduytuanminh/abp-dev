using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.MultiTenancy;
using SmartSoftware.OpenIddict.Applications;
using SmartSoftware.OpenIddict.Authorizations;
using SmartSoftware.OpenIddict.Scopes;
using SmartSoftware.OpenIddict.Tokens;

namespace SmartSoftware.OpenIddict.EntityFrameworkCore;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareOpenIddictDbProperties.ConnectionStringName)]
public interface IOpenIddictDbContext : IEfCoreDbContext
{
    DbSet<OpenIddictApplication> Applications { get; }

    DbSet<OpenIddictAuthorization> Authorizations { get; }

    DbSet<OpenIddictScope> Scopes { get; }

    DbSet<OpenIddictToken> Tokens { get; }
}
