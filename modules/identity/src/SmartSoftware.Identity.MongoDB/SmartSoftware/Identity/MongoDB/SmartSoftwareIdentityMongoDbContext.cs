using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace SmartSoftware.Identity.MongoDB;

[ConnectionStringName(SmartSoftwareIdentityDbProperties.ConnectionStringName)]
public class SmartSoftwareIdentityMongoDbContext : SmartSoftwareMongoDbContext, ISmartSoftwareIdentityMongoDbContext
{
    public IMongoCollection<IdentityUser> Users => Collection<IdentityUser>();

    public IMongoCollection<IdentityRole> Roles => Collection<IdentityRole>();

    public IMongoCollection<IdentityClaimType> ClaimTypes => Collection<IdentityClaimType>();

    public IMongoCollection<OrganizationUnit> OrganizationUnits => Collection<OrganizationUnit>();

    public IMongoCollection<IdentitySecurityLog> SecurityLogs => Collection<IdentitySecurityLog>();

    public IMongoCollection<IdentityLinkUser> LinkUsers => Collection<IdentityLinkUser>();

    public IMongoCollection<IdentityUserDelegation> UserDelegations => Collection<IdentityUserDelegation>();

    public IMongoCollection<IdentitySession> Sessions => Collection<IdentitySession>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureIdentity();
    }
}
