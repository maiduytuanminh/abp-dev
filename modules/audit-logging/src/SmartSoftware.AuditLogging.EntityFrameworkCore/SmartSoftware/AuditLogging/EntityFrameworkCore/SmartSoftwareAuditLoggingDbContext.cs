using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.AuditLogging.EntityFrameworkCore;

[ConnectionStringName(SmartSoftwareAuditLoggingDbProperties.ConnectionStringName)]
public class SmartSoftwareAuditLoggingDbContext : SmartSoftwareDbContext<SmartSoftwareAuditLoggingDbContext>, IAuditLoggingDbContext
{
    public DbSet<AuditLog> AuditLogs { get; set; }

    public SmartSoftwareAuditLoggingDbContext(DbContextOptions<SmartSoftwareAuditLoggingDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAuditLogging();
    }
}
