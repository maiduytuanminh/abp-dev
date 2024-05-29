using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Modularity;
using static SmartSoftware.Identity.AspNetCore.SmartSoftwareSecurityStampValidatorCallback;

namespace SmartSoftware.Identity.AspNetCore;

[DependsOn(
    typeof(SmartSoftwareIdentityDomainModule)
    )]
public class SmartSoftwareIdentityAspNetCoreModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IdentityBuilder>(builder =>
        {
            builder
                .AddDefaultTokenProviders()
                .AddTokenProvider<LinkUserTokenProvider>(LinkUserTokenProviderConsts.LinkUserTokenProviderName)
                .AddSignInManager<SmartSoftwareSignInManager>()
                .AddUserValidator<SmartSoftwareIdentityUserValidator>();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //(TODO: Extract an extension method like IdentityBuilder.AddSmartSoftwareSecurityStampValidator())
        context.Services.AddScoped<SmartSoftwareSecurityStampValidator>();
        context.Services.AddScoped(typeof(SecurityStampValidator<IdentityUser>), provider => provider.GetService(typeof(SmartSoftwareSecurityStampValidator)));
        context.Services.AddScoped(typeof(ISecurityStampValidator), provider => provider.GetService(typeof(SmartSoftwareSecurityStampValidator)));

        var options = context.Services.ExecutePreConfiguredActions(new SmartSoftwareIdentityAspNetCoreOptions());

        if (options.ConfigureAuthentication)
        {
            context.Services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = IdentityConstants.ApplicationScheme;
                    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();
        }
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddOptions<SecurityStampValidatorOptions>()
            .Configure<IServiceProvider>((securityStampValidatorOptions, serviceProvider) =>
            {
                var ssRefreshingPrincipalOptions = serviceProvider.GetRequiredService<IOptions<SmartSoftwareRefreshingPrincipalOptions>>().Value;
                securityStampValidatorOptions.UpdatePrincipal(ssRefreshingPrincipalOptions);
            });
    }
}
