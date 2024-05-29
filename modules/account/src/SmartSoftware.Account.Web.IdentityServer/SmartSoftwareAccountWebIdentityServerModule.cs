using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Identity.AspNetCore;
using SmartSoftware.IdentityServer;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Account.Web;

[DependsOn(
    typeof(SmartSoftwareAccountWebModule),
    typeof(SmartSoftwareIdentityServerDomainModule)
    )]
public class SmartSoftwareAccountWebIdentityServerModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareIdentityAspNetCoreOptions>(options =>
        {
            options.ConfigureAuthentication = false;
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareAccountWebIdentityServerModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAccountWebIdentityServerModule>();
        });

        Configure<IdentityServerOptions>(options =>
        {
            options.UserInteraction.ConsentUrl = "/Consent";
            options.UserInteraction.ErrorUrl = "/Account/Error";
        });

        //TODO: Try to reuse from SmartSoftwareIdentityAspNetCoreModule
        context.Services
            .AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();
    }
}
