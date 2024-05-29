using Microsoft.EntityFrameworkCore;
using SmartSoftware.Auditing.App.Entities;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Modeling;

namespace SmartSoftware.Auditing.App.EntityFrameworkCore;

public class SmartSoftwareAuditingTestDbContext : SmartSoftwareDbContext<SmartSoftwareAuditingTestDbContext>
{
    public DbSet<AppEntityWithAudited> AppEntityWithAudited { get; set; }

    public DbSet<AppEntityWithAuditedAndPropertyHasDisableAuditing> AppEntityWithAuditedAndPropertyHasDisableAuditing { get; set; }

    public DbSet<AppEntityWithDisableAuditing> AppEntityWithDisableAuditing { get; set; }

    public DbSet<AppEntityWithDisableAuditingAndPropertyHasAudited> AppEntityWithDisableAuditingAndPropertyHasAudited { get; set; }

    public DbSet<AppEntityWithPropertyHasAudited> AppEntityWithPropertyHasAudited { get; set; }

    public DbSet<AppEntityWithSelector> AppEntityWithSelector { get; set; }

    public DbSet<AppFullAuditedEntityWithAudited> AppFullAuditedEntityWithAudited { get; set; }

    public DbSet<AppEntityWithAuditedAndHasCustomAuditingProperties> AppEntityWithAuditedAndHasCustomAuditingProperties { get; set; }

    public DbSet<AppEntityWithSoftDelete> AppEntityWithSoftDelete { get; set; }

    public DbSet<AppEntityWithValueObject> AppEntityWithValueObject { get; set; }

    public DbSet<AppEntityWithNavigations> AppEntityWithNavigations { get; set; }

    public SmartSoftwareAuditingTestDbContext(DbContextOptions<SmartSoftwareAuditingTestDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppEntityWithValueObject>(b =>
        {
            b.ConfigureByConvention();
            b.OwnsOne(v => v.AppEntityWithValueObjectAddress);
        });

        modelBuilder.Entity<AppEntityWithNavigations>(b =>
        {
            b.ConfigureByConvention();
            b.OwnsOne(x => x.AppEntityWithValueObjectAddress);
            b.HasOne(x => x.OneToOne).WithOne().HasForeignKey<AppEntityWithNavigationChildOneToOne>(x => x.Id);
            b.HasMany(x => x.OneToMany).WithOne().HasForeignKey(x => x.AppEntityWithNavigationId);
            b.HasMany(x => x.ManyToMany).WithMany(x => x.ManyToMany).UsingEntity<AppEntityWithNavigationsAndAppEntityWithNavigationChildManyToMany>();
        });

    }
}
