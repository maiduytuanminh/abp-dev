using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Modeling;
using SmartSoftware.OpenIddict.Applications;
using SmartSoftware.OpenIddict.Authorizations;
using SmartSoftware.OpenIddict.Scopes;
using SmartSoftware.OpenIddict.Tokens;

namespace SmartSoftware.OpenIddict.EntityFrameworkCore;

public static class OpenIddictDbContextModelCreatingExtensions
{
    public static void ConfigureOpenIddict(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        if (builder.IsTenantOnlyDatabase())
        {
            return;
        }

        builder.Entity<OpenIddictApplication>(b =>
        {
            b.ToTable(SmartSoftwareOpenIddictDbProperties.DbTablePrefix + "Applications", SmartSoftwareOpenIddictDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasIndex(x => x.ClientId);
                //.IsUnique();

            b.Property(x => x.ApplicationType)
                .HasMaxLength(OpenIddictApplicationConsts.ApplicationTypeMaxLength);

            b.Property(x => x.ClientId)
                .HasMaxLength(OpenIddictApplicationConsts.ClientIdMaxLength);

            b.Property(x => x.ConsentType)
                .HasMaxLength(OpenIddictApplicationConsts.ConsentTypeMaxLength);

            b.Property(x => x.ClientType)
                .HasMaxLength(OpenIddictApplicationConsts.ClientTypeMaxLength);

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<OpenIddictAuthorization>(b =>
        {
            b.ToTable(SmartSoftwareOpenIddictDbProperties.DbTablePrefix + "Authorizations", SmartSoftwareOpenIddictDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasIndex(x => new
            {
                x.ApplicationId,
                x.Status,
                x.Subject,
                x.Type
            });

            b.Property(x => x.Status)
                .HasMaxLength(OpenIddictAuthorizationConsts.StatusMaxLength);

            b.Property(x => x.Subject)
                .HasMaxLength(OpenIddictAuthorizationConsts.SubjectMaxLength);

            b.Property(x => x.Type)
                .HasMaxLength(OpenIddictAuthorizationConsts.TypeMaxLength);

            b.HasOne<OpenIddictApplication>().WithMany().HasForeignKey(x => x.ApplicationId).IsRequired(false);

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<OpenIddictScope>(b =>
        {
            b.ToTable(SmartSoftwareOpenIddictDbProperties.DbTablePrefix + "Scopes", SmartSoftwareOpenIddictDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasIndex(x => x.Name);
                //.IsUnique();

            b.Property(x => x.Name)
                .HasMaxLength(OpenIddictScopeConsts.NameMaxLength);

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<OpenIddictToken>(b =>
        {
            b.ToTable(SmartSoftwareOpenIddictDbProperties.DbTablePrefix + "Tokens", SmartSoftwareOpenIddictDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasIndex(x => x.ReferenceId);
                //.IsUnique();

            b.HasIndex(x => new
            {
                x.ApplicationId,
                x.Status,
                x.Subject,
                x.Type
            });

            b.Property(x => x.ReferenceId)
                .HasMaxLength(OpenIddictTokenConsts.ReferenceIdMaxLength);

            b.Property(x => x.Status)
                .HasMaxLength(OpenIddictTokenConsts.StatusMaxLength);

            b.Property(x => x.Subject)
                .HasMaxLength(OpenIddictTokenConsts.SubjectMaxLength);

            b.Property(x => x.Type)
                .HasMaxLength(OpenIddictTokenConsts.TypeMaxLength);

            b.HasOne<OpenIddictApplication>().WithMany().HasForeignKey(x => x.ApplicationId).IsRequired(false);
            b.HasOne<OpenIddictAuthorization>().WithMany().HasForeignKey(x => x.AuthorizationId).IsRequired(false);

            b.ApplyObjectExtensionMappings();
        });

    }
}
