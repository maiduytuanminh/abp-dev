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
public class OpenIddictMongoDbContext : SmartSoftwareMongoDbContext, IOpenIddictMongoDbContext
{
    public IMongoCollection<OpenIddictApplication> Applications => Collection<OpenIddictApplication>();

    public IMongoCollection<OpenIddictAuthorization> Authorizations => Collection<OpenIddictAuthorization>();

    public IMongoCollection<OpenIddictScope> Scopes => Collection<OpenIddictScope>();

    public IMongoCollection<OpenIddictToken> Tokens => Collection<OpenIddictToken>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureOpenIddict();
    }
}
