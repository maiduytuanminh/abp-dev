﻿using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Modeling;

namespace SmartSoftware.BlobStoring.Database.EntityFrameworkCore;

public static class BlobStoringDbContextModelCreatingExtensions
{
    public static void ConfigureBlobStoring(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<DatabaseBlobContainer>(b =>
        {
            b.ToTable(SmartSoftwareBlobStoringDatabaseDbProperties.DbTablePrefix + "BlobContainers", SmartSoftwareBlobStoringDatabaseDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(p => p.Name).IsRequired().HasMaxLength(DatabaseContainerConsts.MaxNameLength);

            b.HasMany<DatabaseBlob>().WithOne().HasForeignKey(p => p.ContainerId);

            b.HasIndex(x => new { x.TenantId, x.Name });

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<DatabaseBlob>(b =>
        {
            b.ToTable(SmartSoftwareBlobStoringDatabaseDbProperties.DbTablePrefix + "Blobs", SmartSoftwareBlobStoringDatabaseDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(p => p.ContainerId).IsRequired(); //TODO: Foreign key!
                b.Property(p => p.Name).IsRequired().HasMaxLength(DatabaseBlobConsts.MaxNameLength);
            b.Property(p => p.Content).HasMaxLength(DatabaseBlobConsts.MaxContentLength);

            b.HasOne<DatabaseBlobContainer>().WithMany().HasForeignKey(p => p.ContainerId);

            b.HasIndex(x => new { x.TenantId, x.ContainerId, x.Name });

            b.ApplyObjectExtensionMappings();
        });

        builder.TryConfigureObjectExtensions<BlobStoringDbContext>();
    }
}