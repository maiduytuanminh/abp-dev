using System.Threading.Tasks;
using IdentityServer4.Configuration;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SmartSoftware.AutoMapper;
using SmartSoftware.BackgroundWorkers;
using SmartSoftware.Caching;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.Identity;
using SmartSoftware.IdentityServer.ApiResources;
using SmartSoftware.IdentityServer.AspNetIdentity;
using SmartSoftware.IdentityServer.Clients;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.IdentityServer.IdentityResources;
using SmartSoftware.IdentityServer.Tokens;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Security;
using SmartSoftware.Security.Claims;
using SmartSoftware.Validation;
using SmartSoftware.Threading;

namespace SmartSoftware.IdentityServer;

[DependsOn(
    typeof(SmartSoftwareIdentityServerDomainSharedModule),
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwareIdentityDomainModule),
    typeof(SmartSoftwareSecurityModule),
    typeof(SmartSoftwareCachingModule),
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareBackgroundWorkersModule)
    )]
public class SmartSoftwareIdentityServerDomainModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareIdentityServerDomainModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<IdentityServerAutoMapperProfile>(validate: true);
        });

        Configure<SmartSoftwareDistributedEntityEventOptions>(options =>
        {
            options.EtoMappings.Add<ApiResource, ApiResourceEto>(typeof(SmartSoftwareIdentityServerDomainModule));
            options.EtoMappings.Add<Client, ClientEto>(typeof(SmartSoftwareIdentityServerDomainModule));
            options.EtoMappings.Add<DeviceFlowCodes, DeviceFlowCodesEto>(typeof(SmartSoftwareIdentityServerDomainModule));
            options.EtoMappings.Add<IdentityResource, IdentityResourceEto>(typeof(SmartSoftwareIdentityServerDomainModule));
        });

        Configure<SmartSoftwareClaimsServiceOptions>(options =>
        {
            options.RequestedClaims.AddRange(new[]{
                    SmartSoftwareClaimTypes.TenantId,
                    SmartSoftwareClaimTypes.EditionId
            });
        });

        AddIdentityServer(context.Services);
    }

    private static void AddIdentityServer(IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        var builderOptions = services.ExecutePreConfiguredActions<SmartSoftwareIdentityServerBuilderOptions>();

        var identityServerBuilder = AddIdentityServer(services, builderOptions);

        if (builderOptions.AddDeveloperSigningCredential)
        {
            identityServerBuilder = identityServerBuilder.AddDeveloperSigningCredential();
        }

        identityServerBuilder.AddSmartSoftwareIdentityServer(builderOptions);

        services.ExecutePreConfiguredActions(identityServerBuilder);

        if (!services.IsAdded<IPersistedGrantService>())
        {
            services.TryAddSingleton<IPersistedGrantStore, InMemoryPersistedGrantStore>();
        }

        if (!services.IsAdded<IDeviceFlowStore>())
        {
            services.TryAddSingleton<IDeviceFlowStore, InMemoryDeviceFlowStore>();
        }

        if (!services.IsAdded<IClientStore>())
        {
            identityServerBuilder.AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"));
        }

        if (!services.IsAdded<IResourceStore>())
        {
            identityServerBuilder.AddInMemoryApiResources(configuration.GetSection("IdentityServer:ApiResources"));
            identityServerBuilder.AddInMemoryIdentityResources(configuration.GetSection("IdentityServer:IdentityResources"));
        }
    }

    private static IIdentityServerBuilder AddIdentityServer(IServiceCollection services, SmartSoftwareIdentityServerBuilderOptions ssIdentityServerBuilderOptions)
    {
        services.Configure<IdentityServerOptions>(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
        });

        var identityServerBuilder = services.AddIdentityServerBuilder()
            .AddRequiredPlatformServices()
            .AddCoreServices()
            .AddDefaultEndpoints()
            .AddPluggableServices()
            .AddValidators()
            .AddResponseGenerators()
            .AddDefaultSecretParsers()
            .AddDefaultSecretValidators();

        if (ssIdentityServerBuilderOptions.AddIdentityServerCookieAuthentication)
        {
            identityServerBuilder.AddCookieAuthentication();
        }

        // provide default in-memory implementation, not suitable for most production scenarios
        identityServerBuilder.AddInMemoryPersistedGrants();

        return identityServerBuilder;
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityServerModuleExtensionConsts.ModuleName,
                IdentityServerModuleExtensionConsts.EntityNames.Client,
                typeof(Client)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityServerModuleExtensionConsts.ModuleName,
                IdentityServerModuleExtensionConsts.EntityNames.IdentityResource,
                typeof(IdentityResource)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityServerModuleExtensionConsts.ModuleName,
                IdentityServerModuleExtensionConsts.EntityNames.ApiResource,
                typeof(ApiResource)
            );
        });
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<TokenCleanupOptions>>().Value;
        if (options.IsCleanupEnabled)
        {
            await context.ServiceProvider
                .GetRequiredService<IBackgroundWorkerManager>()
                .AddAsync(
                    context.ServiceProvider
                        .GetRequiredService<TokenCleanupBackgroundWorker>()
                );
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }
}
