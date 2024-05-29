using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Modeling;
using SmartSoftware.IdentityServer.ApiResources;
using SmartSoftware.IdentityServer.ApiScopes;
using SmartSoftware.IdentityServer.Clients;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.IdentityServer.Grants;
using SmartSoftware.IdentityServer.IdentityResources;

namespace SmartSoftware.IdentityServer.EntityFrameworkCore;

public static class IdentityServerDbContextModelCreatingExtensions
{
    public static void ConfigureIdentityServer(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        if (builder.IsTenantOnlyDatabase())
        {
            return;
        }

        #region Client

        builder.Entity<Client>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "Clients", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.ClientId).HasMaxLength(ClientConsts.ClientIdMaxLength).IsRequired();
            b.Property(x => x.ProtocolType).HasMaxLength(ClientConsts.ProtocolTypeMaxLength).IsRequired();
            b.Property(x => x.ClientName).HasMaxLength(ClientConsts.ClientNameMaxLength);
            b.Property(x => x.ClientUri).HasMaxLength(ClientConsts.ClientUriMaxLength);
            b.Property(x => x.LogoUri).HasMaxLength(ClientConsts.LogoUriMaxLength);
            b.Property(x => x.Description).HasMaxLength(ClientConsts.DescriptionMaxLength);
            b.Property(x => x.FrontChannelLogoutUri).HasMaxLength(ClientConsts.FrontChannelLogoutUriMaxLength);
            b.Property(x => x.BackChannelLogoutUri).HasMaxLength(ClientConsts.BackChannelLogoutUriMaxLength);
            b.Property(x => x.ClientClaimsPrefix).HasMaxLength(ClientConsts.ClientClaimsPrefixMaxLength);
            b.Property(x => x.PairWiseSubjectSalt).HasMaxLength(ClientConsts.PairWiseSubjectSaltMaxLength);
            b.Property(x => x.UserCodeType).HasMaxLength(ClientConsts.UserCodeTypeMaxLength);
            b.Property(x => x.AllowedIdentityTokenSigningAlgorithms).HasMaxLength(ClientConsts.AllowedIdentityTokenSigningAlgorithms);

            b.HasMany(x => x.AllowedScopes).WithOne().HasForeignKey(x => x.ClientId).IsRequired();
            b.HasMany(x => x.ClientSecrets).WithOne().HasForeignKey(x => x.ClientId).IsRequired();
            b.HasMany(x => x.AllowedGrantTypes).WithOne().HasForeignKey(x => x.ClientId).IsRequired();
            b.HasMany(x => x.AllowedCorsOrigins).WithOne().HasForeignKey(x => x.ClientId).IsRequired();
            b.HasMany(x => x.RedirectUris).WithOne().HasForeignKey(x => x.ClientId).IsRequired();
            b.HasMany(x => x.PostLogoutRedirectUris).WithOne().HasForeignKey(x => x.ClientId).IsRequired();
            b.HasMany(x => x.IdentityProviderRestrictions).WithOne().HasForeignKey(x => x.ClientId).IsRequired();
            b.HasMany(x => x.Claims).WithOne().HasForeignKey(x => x.ClientId).IsRequired();
            b.HasMany(x => x.Properties).WithOne().HasForeignKey(x => x.ClientId).IsRequired();

            b.HasIndex(x => x.ClientId);

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ClientGrantType>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ClientGrantTypes", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ClientId, x.GrantType });

            b.Property(x => x.GrantType).HasMaxLength(ClientGrantTypeConsts.GrantTypeMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ClientRedirectUri>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ClientRedirectUris", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ClientId, x.RedirectUri });

            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql))
            {
                ClientRedirectUriConsts.RedirectUriMaxLengthValue = 300;
            }

            b.Property(x => x.RedirectUri).HasMaxLength(ClientRedirectUriConsts.RedirectUriMaxLengthValue).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ClientPostLogoutRedirectUri>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ClientPostLogoutRedirectUris", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ClientId, x.PostLogoutRedirectUri });

            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql))
            {
                ClientPostLogoutRedirectUriConsts.PostLogoutRedirectUriMaxLengthValue = 300;
            }

            b.Property(x => x.PostLogoutRedirectUri)
                .HasMaxLength(ClientPostLogoutRedirectUriConsts.PostLogoutRedirectUriMaxLengthValue)
                .IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ClientScope>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ClientScopes", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ClientId, x.Scope });

            b.Property(x => x.Scope).HasMaxLength(ClientScopeConsts.ScopeMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ClientSecret>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ClientSecrets", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ClientId, x.Type, x.Value });

            b.Property(x => x.Type).HasMaxLength(ClientSecretConsts.TypeMaxLength).IsRequired();
            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql, EfCoreDatabaseProvider.Oracle))
            {
                ClientSecretConsts.ValueMaxLength = 300;
            }
            b.Property(x => x.Value).HasMaxLength(ClientSecretConsts.ValueMaxLength).IsRequired();
            b.Property(x => x.Description).HasMaxLength(ClientSecretConsts.DescriptionMaxLength);

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ClientClaim>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ClientClaims", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ClientId, x.Type, x.Value });

            b.Property(x => x.Type).HasMaxLength(ClientClaimConsts.TypeMaxLength).IsRequired();
            b.Property(x => x.Value).HasMaxLength(ClientClaimConsts.ValueMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ClientIdPRestriction>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ClientIdPRestrictions", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ClientId, x.Provider });

            b.Property(x => x.Provider).HasMaxLength(ClientIdPRestrictionConsts.ProviderMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ClientCorsOrigin>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ClientCorsOrigins", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ClientId, x.Origin });

            b.Property(x => x.Origin).HasMaxLength(ClientCorsOriginConsts.OriginMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ClientProperty>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ClientProperties", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ClientId, x.Key, x.Value });

            b.Property(x => x.Key).HasMaxLength(ClientPropertyConsts.KeyMaxLength).IsRequired();
            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql))
            {
                ClientPropertyConsts.ValueMaxLength = 300;
            }
            b.Property(x => x.Value).HasMaxLength(ClientPropertyConsts.ValueMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        #endregion

        #region IdentityResource

        builder.Entity<IdentityResource>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "IdentityResources", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name).HasMaxLength(IdentityResourceConsts.NameMaxLength).IsRequired();
            b.Property(x => x.DisplayName).HasMaxLength(IdentityResourceConsts.DisplayNameMaxLength);
            b.Property(x => x.Description).HasMaxLength(IdentityResourceConsts.DescriptionMaxLength);

            b.HasMany(x => x.UserClaims).WithOne().HasForeignKey(x => x.IdentityResourceId).IsRequired();
            b.HasMany(x => x.Properties).WithOne().HasForeignKey(x => x.IdentityResourceId).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<IdentityResourceClaim>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "IdentityResourceClaims", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.IdentityResourceId, x.Type });

            b.Property(x => x.Type).HasMaxLength(UserClaimConsts.TypeMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<IdentityResourceProperty>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "IdentityResourceProperties", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.IdentityResourceId, x.Key, x.Value });

            b.Property(x => x.Key).HasMaxLength(IdentityResourcePropertyConsts.KeyMaxLength).IsRequired();
            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql, EfCoreDatabaseProvider.Oracle))
            {
                IdentityResourcePropertyConsts.ValueMaxLength = 300;
            }
            b.Property(x => x.Value).HasMaxLength(IdentityResourcePropertyConsts.ValueMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        #endregion

        #region ApiResource

        builder.Entity<ApiResource>(b =>
       {
           b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiResources", SmartSoftwareIdentityServerDbProperties.DbSchema);

           b.ConfigureByConvention();

           b.Property(x => x.Name).HasMaxLength(ApiResourceConsts.NameMaxLength).IsRequired();
           b.Property(x => x.DisplayName).HasMaxLength(ApiResourceConsts.DisplayNameMaxLength);
           b.Property(x => x.Description).HasMaxLength(ApiResourceConsts.DescriptionMaxLength);
           b.Property(x => x.AllowedAccessTokenSigningAlgorithms).HasMaxLength(ApiResourceConsts.AllowedAccessTokenSigningAlgorithmsMaxLength);

           b.HasMany(x => x.Secrets).WithOne().HasForeignKey(x => x.ApiResourceId).IsRequired();
           b.HasMany(x => x.Scopes).WithOne().HasForeignKey(x => x.ApiResourceId).IsRequired();
           b.HasMany(x => x.UserClaims).WithOne().HasForeignKey(x => x.ApiResourceId).IsRequired();
           b.HasMany(x => x.Properties).WithOne().HasForeignKey(x => x.ApiResourceId).IsRequired();

           b.ApplyObjectExtensionMappings();
       });

        builder.Entity<ApiResourceSecret>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiResourceSecrets", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ApiResourceId, x.Type, x.Value });

            b.Property(x => x.Type).HasMaxLength(ApiResourceSecretConsts.TypeMaxLength).IsRequired();

            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql, EfCoreDatabaseProvider.Oracle))
            {
                ApiResourceSecretConsts.ValueMaxLength = 300;
            }
            b.Property(x => x.Value).HasMaxLength(ApiResourceSecretConsts.ValueMaxLength).IsRequired();

            b.Property(x => x.Description).HasMaxLength(ApiResourceSecretConsts.DescriptionMaxLength);

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ApiResourceClaim>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiResourceClaims", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ApiResourceId, x.Type });

            b.Property(x => x.Type).HasMaxLength(UserClaimConsts.TypeMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ApiResourceScope>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiResourceScopes", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ApiResourceId, x.Scope });

            b.Property(x => x.Scope).HasMaxLength(ApiResourceScopeConsts.ScopeMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ApiResourceProperty>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiResourceProperties", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ApiResourceId, x.Key, x.Value });

            b.Property(x => x.Key).HasMaxLength(ApiResourcePropertyConsts.KeyMaxLength).IsRequired();
            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql, EfCoreDatabaseProvider.Oracle))
            {
                ApiResourcePropertyConsts.ValueMaxLength = 300;
            }
            b.Property(x => x.Value).HasMaxLength(ApiResourcePropertyConsts.ValueMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        #endregion

        #region ApiScope

        builder.Entity<ApiScope>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiScopes", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name).HasMaxLength(ApiScopeConsts.NameMaxLength).IsRequired();
            b.Property(x => x.DisplayName).HasMaxLength(ApiScopeConsts.DisplayNameMaxLength);
            b.Property(x => x.Description).HasMaxLength(ApiScopeConsts.DescriptionMaxLength);

            b.HasMany(x => x.UserClaims).WithOne().HasForeignKey(x => x.ApiScopeId).IsRequired();
            b.HasMany(x => x.Properties).WithOne().HasForeignKey(x => x.ApiScopeId).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ApiScopeClaim>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiScopeClaims", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ApiScopeId, x.Type });

            b.Property(x => x.Type).HasMaxLength(UserClaimConsts.TypeMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<ApiScopeProperty>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiScopeProperties", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasKey(x => new { x.ApiScopeId, x.Key, x.Value });

            b.Property(x => x.Key).HasMaxLength(ApiScopePropertyConsts.KeyMaxLength).IsRequired();
            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql, EfCoreDatabaseProvider.Oracle))
            {
                ApiScopePropertyConsts.ValueMaxLength = 300;
            }
            b.Property(x => x.Value).HasMaxLength(ApiScopePropertyConsts.ValueMaxLength).IsRequired();

            b.ApplyObjectExtensionMappings();
        });

        #endregion

        #region PersistedGrant

        builder.Entity<PersistedGrant>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "PersistedGrants", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Key).HasMaxLength(PersistedGrantConsts.KeyMaxLength).ValueGeneratedNever();
            b.Property(x => x.Type).HasMaxLength(PersistedGrantConsts.TypeMaxLength).IsRequired();
            b.Property(x => x.SubjectId).HasMaxLength(PersistedGrantConsts.SubjectIdMaxLength);
            b.Property(x => x.SessionId).HasMaxLength(PersistedGrantConsts.SessionIdMaxLength);
            b.Property(x => x.ClientId).HasMaxLength(PersistedGrantConsts.ClientIdMaxLength).IsRequired();
            b.Property(x => x.Description).HasMaxLength(PersistedGrantConsts.DescriptionMaxLength);
            b.Property(x => x.CreationTime).IsRequired();

            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql))
            {
                PersistedGrantConsts.DataMaxLengthValue = 10000; //TODO: MySQL accepts 20.000. We can consider to change in v3.0.
                }

            b.Property(x => x.Data).HasMaxLength(PersistedGrantConsts.DataMaxLengthValue).IsRequired();

            b.HasKey(x => x.Key); //TODO: What about Id!!!

                b.HasIndex(x => new { x.SubjectId, x.ClientId, x.Type });
            b.HasIndex(x => new { x.SubjectId, x.SessionId, x.Type });
            b.HasIndex(x => x.Expiration);

            b.ApplyObjectExtensionMappings();
        });

        #endregion

        #region DeviceFlowCodes

        builder.Entity<DeviceFlowCodes>(b =>
        {
            b.ToTable(SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "DeviceFlowCodes", SmartSoftwareIdentityServerDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.DeviceCode).HasMaxLength(DeviceFlowCodesConsts.DeviceCodeMaxLength).IsRequired();
            b.Property(x => x.UserCode).HasMaxLength(DeviceFlowCodesConsts.UserCodeMaxLength).IsRequired();
            b.Property(x => x.SubjectId).HasMaxLength(DeviceFlowCodesConsts.SubjectIdMaxLength);
            b.Property(x => x.SessionId).HasMaxLength(DeviceFlowCodesConsts.SessionIdMaxLength);
            b.Property(x => x.ClientId).HasMaxLength(DeviceFlowCodesConsts.ClientIdMaxLength).IsRequired();
            b.Property(x => x.Description).HasMaxLength(DeviceFlowCodesConsts.DescriptionMaxLength);
            b.Property(x => x.CreationTime).IsRequired();
            b.Property(x => x.Expiration).IsRequired();

            if (IsDatabaseProvider(builder, EfCoreDatabaseProvider.MySql))
            {
                DeviceFlowCodesConsts.DataMaxLength = 10000; //TODO: MySQL accepts 20.000. We can consider to change in v3.0.
                }
            b.Property(x => x.Data).HasMaxLength(DeviceFlowCodesConsts.DataMaxLength).IsRequired();

            b.HasIndex(x => new { x.UserCode });
            b.HasIndex(x => x.DeviceCode).IsUnique();
            b.HasIndex(x => x.Expiration);

            b.ApplyObjectExtensionMappings();
        });

        #endregion

        builder.TryConfigureObjectExtensions<IdentityServerDbContext>();
    }

    private static bool IsDatabaseProvider(
        ModelBuilder modelBuilder,
        params EfCoreDatabaseProvider[] providers)
    {
        foreach (var provider in providers)
        {
            if (modelBuilder.GetDatabaseProvider() == provider)
            {
                return true;
            }
        }

        return false;
    }
}
