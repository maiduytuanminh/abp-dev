using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Demo.Server.EntityFrameworkCore;
using OpenIddict.Demo.Server.ExtensionGrants;
using SmartSoftware;
using SmartSoftware.Account;
using SmartSoftware.Account.Web;
using SmartSoftware.AspNetCore.MultiTenancy;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic;
using SmartSoftware.Autofac;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.FeatureManagement;
using SmartSoftware.FeatureManagement.EntityFrameworkCore;
using SmartSoftware.Identity;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.Identity.Web;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.OpenIddict;
using SmartSoftware.OpenIddict.EntityFrameworkCore;
using SmartSoftware.OpenIddict.ExtensionGrantTypes;
using SmartSoftware.OpenIddict.WildcardDomains;
using SmartSoftware.PermissionManagement;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.HttpApi;
using SmartSoftware.PermissionManagement.Identity;
using SmartSoftware.SettingManagement;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.SettingManagement.Web;
using SmartSoftware.TenantManagement;
using SmartSoftware.TenantManagement.EntityFrameworkCore;
using SmartSoftware.TenantManagement.Web;

namespace OpenIddict.Demo.Server;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule),
    typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule),
    typeof(SmartSoftwareAspNetCoreMultiTenancyModule),

    typeof(SmartSoftwareOpenIddictAspNetCoreModule),
    typeof(SmartSoftwareOpenIddictEntityFrameworkCoreModule),

    typeof(SmartSoftwareAccountApplicationModule),
    typeof(SmartSoftwareAccountHttpApiModule),
    typeof(SmartSoftwareAccountWebOpenIddictModule),

    typeof(SmartSoftwareTenantManagementApplicationModule),
    typeof(SmartSoftwareTenantManagementHttpApiModule),
    typeof(SmartSoftwareTenantManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareTenantManagementWebModule),

    typeof(SmartSoftwarePermissionManagementDomainIdentityModule),
    typeof(SmartSoftwareIdentityApplicationModule),
    typeof(SmartSoftwareIdentityHttpApiModule),
    typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
    typeof(SmartSoftwareIdentityWebModule),

    typeof(SmartSoftwarePermissionManagementApplicationModule),
    typeof(SmartSoftwarePermissionManagementHttpApiModule),
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),

    typeof(SmartSoftwareFeatureManagementApplicationModule),
    typeof(SmartSoftwareFeatureManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareFeatureManagementHttpApiModule),
    typeof(SmartSoftwareFeatureManagementWebModule),

    typeof(SmartSoftwareSettingManagementApplicationModule),
    typeof(SmartSoftwareSettingManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareSettingManagementHttpApiModule),
    typeof(SmartSoftwareSettingManagementWebModule)
)]
public class OpenIddictServerModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<SmartSoftwareOpenIddictAspNetCoreOptions>(options =>
        {
            //https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html
            options.AddDevelopmentEncryptionAndSigningCertificate = false;
        });

        PreConfigure<OpenIddictServerBuilder>(builder =>
        {
            //https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html
            using (var algorithm = RSA.Create(keySizeInBits: 2048))
            {
                var subject = new X500DistinguishedName("CN=Fabrikam Encryption Certificate");
                var request = new CertificateRequest(subject, algorithm, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, critical: true));
                var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));
                builder.AddSigningCertificate(certificate);
            }

            using (var algorithm = RSA.Create(keySizeInBits: 2048))
            {
                var subject = new X500DistinguishedName("CN=Fabrikam Signing Certificate");
                var request = new CertificateRequest(subject, algorithm, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment, critical: true));
                var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));
                builder.AddEncryptionCertificate(certificate);
            }

            builder.Configure(openIddictServerOptions =>
            {
                openIddictServerOptions.GrantTypes.Add(MyTokenExtensionGrant.ExtensionGrantName);
            });
        });

        PreConfigure<SmartSoftwareOpenIddictWildcardDomainOptions>(options =>
        {
            options.EnableWildcardDomainSupport = true;
            options.WildcardDomainsFormat.Add("https://{0}.smartsoftware.io/signin-oidc");
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("SmartSoftwareAPIResource");

                options.UseLocalServer();

                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<ServerDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        Configure<SmartSoftwareMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });

        Configure<SmartSoftwareOpenIddictExtensionGrantsOptions>(options =>
        {
            options.Grants.Add(MyTokenExtensionGrant.ExtensionGrantName, new MyTokenExtensionGrant());
        });
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<IDataSeeder>()
            .SeedAsync();

        var tenantManager = context.ServiceProvider.GetRequiredService<TenantManager>();
        var tenantRepository = context.ServiceProvider.GetRequiredService<ITenantRepository>();
        var tenant = await tenantRepository.FindByNameAsync("Default") ??
                     await tenantRepository.InsertAsync(await tenantManager.CreateAsync("Default"));
        await context.ServiceProvider.GetRequiredService<IDataSeeder>().SeedAsync(tenant.Id);
    }
}
