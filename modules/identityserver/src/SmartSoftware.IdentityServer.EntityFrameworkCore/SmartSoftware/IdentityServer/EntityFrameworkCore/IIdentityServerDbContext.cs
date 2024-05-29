using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.IdentityServer.ApiResources;
using SmartSoftware.IdentityServer.ApiScopes;
using SmartSoftware.IdentityServer.Clients;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.IdentityServer.Grants;
using SmartSoftware.IdentityServer.IdentityResources;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.IdentityServer.EntityFrameworkCore;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareIdentityServerDbProperties.ConnectionStringName)]
public interface IIdentityServerDbContext : IEfCoreDbContext
{
    #region ApiResource

    DbSet<ApiResource> ApiResources { get; }

    DbSet<ApiResourceSecret> ApiResourceSecrets { get; }

    DbSet<ApiResourceClaim> ApiResourceClaims { get; }

    DbSet<ApiResourceScope> ApiResourceScopes { get; }

    DbSet<ApiResourceProperty> ApiResourceProperties { get; }

    #endregion

    #region ApiScope

    DbSet<ApiScope> ApiScopes { get; }

    DbSet<ApiScopeClaim> ApiScopeClaims { get; }

    DbSet<ApiScopeProperty> ApiScopeProperties { get; }

    #endregion

    #region IdentityResource

    DbSet<IdentityResource> IdentityResources { get; }

    DbSet<IdentityResourceClaim> IdentityClaims { get; }

    DbSet<IdentityResourceProperty> IdentityResourceProperties { get; }

    #endregion

    #region Client

    DbSet<Client> Clients { get; }

    DbSet<ClientGrantType> ClientGrantTypes { get; }

    DbSet<ClientRedirectUri> ClientRedirectUris { get; }

    DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; }

    DbSet<ClientScope> ClientScopes { get; }

    DbSet<ClientSecret> ClientSecrets { get; }

    DbSet<ClientClaim> ClientClaims { get; }

    DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; }

    DbSet<ClientCorsOrigin> ClientCorsOrigins { get; }

    DbSet<ClientProperty> ClientProperties { get; }

    #endregion

    DbSet<PersistedGrant> PersistedGrants { get; }

    DbSet<DeviceFlowCodes> DeviceFlowCodes { get; }
}
