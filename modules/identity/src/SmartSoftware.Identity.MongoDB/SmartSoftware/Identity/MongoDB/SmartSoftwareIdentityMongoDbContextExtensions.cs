using SmartSoftware.MongoDB;

namespace SmartSoftware.Identity.MongoDB;

public static class SmartSoftwareIdentityMongoDbContextExtensions
{
    public static void ConfigureIdentity(this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<IdentityUser>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityDbProperties.DbTablePrefix + "Users";
        });

        builder.Entity<IdentityRole>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityDbProperties.DbTablePrefix + "Roles";
        });

        builder.Entity<IdentityClaimType>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityDbProperties.DbTablePrefix + "ClaimTypes";
        });

        builder.Entity<OrganizationUnit>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityDbProperties.DbTablePrefix + "OrganizationUnits";
        });

        builder.Entity<IdentitySecurityLog>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityDbProperties.DbTablePrefix + "SecurityLogs";
        });

        builder.Entity<IdentityLinkUser>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityDbProperties.DbTablePrefix + "LinkUsers";
        });

        builder.Entity<IdentityUserDelegation>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityDbProperties.DbTablePrefix + "UserDelegations";
        });

        builder.Entity<IdentitySession>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityDbProperties.DbTablePrefix + "Sessions";
        });
    }
}
