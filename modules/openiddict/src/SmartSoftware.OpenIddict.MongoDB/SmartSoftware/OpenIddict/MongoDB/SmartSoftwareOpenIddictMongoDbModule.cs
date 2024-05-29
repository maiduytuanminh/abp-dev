using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;
using SmartSoftware.OpenIddict.Applications;
using SmartSoftware.OpenIddict.Authorizations;
using SmartSoftware.OpenIddict.Scopes;
using SmartSoftware.OpenIddict.Tokens;

namespace SmartSoftware.OpenIddict.MongoDB;

[DependsOn(
    typeof(SmartSoftwareOpenIddictDomainModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class SmartSoftwareOpenIddictMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<OpenIddictMongoDbContext>(options =>
        {
            options.AddDefaultRepositories<IOpenIddictMongoDbContext>();

            options.AddRepository<OpenIddictApplication, MongoOpenIddictApplicationRepository>();
            options.AddRepository<OpenIddictAuthorization, MongoOpenIddictAuthorizationRepository>();
            options.AddRepository<OpenIddictScope, MongoOpenIddictScopeRepository>();
            options.AddRepository<OpenIddictToken, MongoOpenIddictTokenRepository>();
        });
    }
}
