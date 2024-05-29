using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;
using SmartSoftware.OpenIddict.Applications;
using SmartSoftware.OpenIddict.Authorizations;
using SmartSoftware.OpenIddict.Scopes;
using SmartSoftware.OpenIddict.Tokens;

namespace SmartSoftware.OpenIddict.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareOpenIddictDbProperties.ConnectionStringName)]
public interface IOpenIddictMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<OpenIddictApplication> Applications { get; }

    IMongoCollection<OpenIddictAuthorization> Authorizations { get; }

    IMongoCollection<OpenIddictScope> Scopes { get; }

    IMongoCollection<OpenIddictToken> Tokens { get; }
}
