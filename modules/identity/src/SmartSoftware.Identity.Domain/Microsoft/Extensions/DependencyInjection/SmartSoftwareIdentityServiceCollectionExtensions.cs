using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareIdentityServiceCollectionExtensions
{
    public static IdentityBuilder AddSmartSoftwareIdentity(this IServiceCollection services)
    {
        return services.AddSmartSoftwareIdentity(setupAction: null);
    }

    public static IdentityBuilder AddSmartSoftwareIdentity(this IServiceCollection services, Action<IdentityOptions> setupAction)
    {
        //SmartSoftwareRoleManager
        services.TryAddScoped<IdentityRoleManager>();
        services.TryAddScoped(typeof(RoleManager<IdentityRole>), provider => provider.GetService(typeof(IdentityRoleManager)));

        //SmartSoftwareUserManager
        services.TryAddScoped<IdentityUserManager>();
        services.TryAddScoped(typeof(UserManager<IdentityUser>), provider => provider.GetService(typeof(IdentityUserManager)));

        //SmartSoftwareUserStore
        services.TryAddScoped<IdentityUserStore>();
        services.TryAddScoped(typeof(IUserStore<IdentityUser>), provider => provider.GetService(typeof(IdentityUserStore)));

        //SmartSoftwareRoleStore
        services.TryAddScoped<IdentityRoleStore>();
        services.TryAddScoped(typeof(IRoleStore<IdentityRole>), provider => provider.GetService(typeof(IdentityRoleStore)));

        return services
            .AddIdentityCore<IdentityUser>(setupAction)
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<SmartSoftwareUserClaimsPrincipalFactory>();
    }
}
