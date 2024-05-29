using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace SmartSoftware.Identity.MongoDB;

[ConnectionStringName(SmartSoftwareIdentityDbProperties.ConnectionStringName)]
public interface ISmartSoftwareIdentityMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<IdentityUser> Users { get; }

    IMongoCollection<IdentityRole> Roles { get; }

    IMongoCollection<IdentityClaimType> ClaimTypes { get; }

    IMongoCollection<OrganizationUnit> OrganizationUnits { get; }

    IMongoCollection<IdentitySecurityLog> SecurityLogs { get; }

    IMongoCollection<IdentityLinkUser> LinkUsers { get; }

    IMongoCollection<IdentityUserDelegation> UserDelegations { get; }

    IMongoCollection<IdentitySession> Sessions { get; }
}
