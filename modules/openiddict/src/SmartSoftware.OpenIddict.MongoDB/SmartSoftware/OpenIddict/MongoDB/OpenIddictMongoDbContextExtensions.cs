using SmartSoftware.MongoDB;
using SmartSoftware.OpenIddict.Applications;
using SmartSoftware.OpenIddict.Authorizations;
using SmartSoftware.OpenIddict.Scopes;
using SmartSoftware.OpenIddict.Tokens;

namespace SmartSoftware.OpenIddict.MongoDB;

public static class OpenIddictMongoDbContextExtensions
{
    public static void ConfigureOpenIddict(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
        
        builder.Entity<OpenIddictApplication>(b =>
        {
            b.CollectionName = SmartSoftwareOpenIddictDbProperties.DbTablePrefix + "Applications";
        });
        
        builder.Entity<OpenIddictAuthorization>(b =>
        {
            b.CollectionName = SmartSoftwareOpenIddictDbProperties.DbTablePrefix + "Authorizations";
        });
        
        builder.Entity<OpenIddictScope>(b =>
        {
            b.CollectionName = SmartSoftwareOpenIddictDbProperties.DbTablePrefix + "Scopes";
        });
        
        builder.Entity<OpenIddictToken>(b =>
        {
            b.CollectionName = SmartSoftwareOpenIddictDbProperties.DbTablePrefix + "Tokens";
        });
    }
}
