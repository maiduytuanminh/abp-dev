using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SmartSoftware.AutoMapper;
using SmartSoftware.Domain;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Security.Claims;
using SmartSoftware.Threading;
using SmartSoftware.Users;

namespace SmartSoftware.Identity;

[DependsOn(
    typeof(SmartSoftwareDddDomainModule),
    typeof(SmartSoftwareIdentityDomainSharedModule),
    typeof(SmartSoftwareUsersDomainModule),
    typeof(SmartSoftwareAutoMapperModule)
    )]
public class SmartSoftwareIdentityDomainModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<SmartSoftwareClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsRemoteRefreshEnabled = false;
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareIdentityDomainModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<IdentityDomainMappingProfile>(validate: true);
        });

        Configure<SmartSoftwareDistributedEntityEventOptions>(options =>
        {
            options.EtoMappings.Add<IdentityUser, UserEto>(typeof(SmartSoftwareIdentityDomainModule));
            options.EtoMappings.Add<IdentityClaimType, IdentityClaimTypeEto>(typeof(SmartSoftwareIdentityDomainModule));
            options.EtoMappings.Add<IdentityRole, IdentityRoleEto>(typeof(SmartSoftwareIdentityDomainModule));
            options.EtoMappings.Add<OrganizationUnit, OrganizationUnitEto>(typeof(SmartSoftwareIdentityDomainModule));

            options.AutoEventSelectors.Add<IdentityUser>();
            options.AutoEventSelectors.Add<IdentityRole>();
        });

        var identityBuilder = context.Services.AddSmartSoftwareIdentity(options =>
        {
            options.User.RequireUniqueEmail = true;
        });

        context.Services.AddObjectAccessor(identityBuilder);
        context.Services.ExecutePreConfiguredActions(identityBuilder);

        Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserIdClaimType = SmartSoftwareClaimTypes.UserId;
            options.ClaimsIdentity.UserNameClaimType = SmartSoftwareClaimTypes.UserName;
            options.ClaimsIdentity.RoleClaimType = SmartSoftwareClaimTypes.Role;
            options.ClaimsIdentity.EmailClaimType = SmartSoftwareClaimTypes.Email;
        });

        context.Services.AddSmartSoftwareDynamicOptions<IdentityOptions, SmartSoftwareIdentityOptionsManager>();
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.User,
                typeof(IdentityUser)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.Role,
                typeof(IdentityRole)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.ClaimType,
                typeof(IdentityClaimType)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.OrganizationUnit,
                typeof(OrganizationUnit)
            );
        });
    }
}
