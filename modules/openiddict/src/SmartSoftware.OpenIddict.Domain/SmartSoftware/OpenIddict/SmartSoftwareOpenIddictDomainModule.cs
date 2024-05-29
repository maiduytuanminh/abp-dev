using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using SmartSoftware.BackgroundWorkers;
using SmartSoftware.Caching;
using SmartSoftware.DistributedLocking;
using SmartSoftware.Domain;
using SmartSoftware.Guids;
using SmartSoftware.Identity;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.OpenIddict.Applications;
using SmartSoftware.OpenIddict.Authorizations;
using SmartSoftware.OpenIddict.Scopes;
using SmartSoftware.OpenIddict.Tokens;
using SmartSoftware.Threading;

namespace SmartSoftware.OpenIddict;

[DependsOn(
    typeof(SmartSoftwareDddDomainModule),
    typeof(SmartSoftwareIdentityDomainModule),
    typeof(SmartSoftwareOpenIddictDomainSharedModule),
    typeof(SmartSoftwareDistributedLockingAbstractionsModule),
    typeof(SmartSoftwareCachingModule),
    typeof(SmartSoftwareGuidsModule)
)]
public class SmartSoftwareOpenIddictDomainModule : SmartSoftwareModule
{
    private readonly static OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        AddOpenIddictCore(context.Services);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<TokenCleanupOptions>>().Value;
        if (options.IsCleanupEnabled)
        {
            await context.ServiceProvider
                .GetRequiredService<IBackgroundWorkerManager>()
                .AddAsync(context.ServiceProvider.GetRequiredService<TokenCleanupBackgroundWorker>());
        }
    }

    private void AddOpenIddictCore(IServiceCollection services)
    {
        var openIddictBuilder = services.AddOpenIddict()
            .AddCore(builder =>
            {
                builder
                    .SetDefaultApplicationEntity<OpenIddictApplicationModel>()
                    .SetDefaultAuthorizationEntity<OpenIddictAuthorizationModel>()
                    .SetDefaultScopeEntity<OpenIddictScopeModel>()
                    .SetDefaultTokenEntity<OpenIddictTokenModel>();

                builder
                    .AddApplicationStore<SmartSoftwareOpenIddictApplicationStore>()
                    .AddAuthorizationStore<SmartSoftwareOpenIddictAuthorizationStore>()
                    .AddScopeStore<SmartSoftwareOpenIddictScopeStore>()
                    .AddTokenStore<SmartSoftwareOpenIddictTokenStore>();

                builder.ReplaceApplicationManager(typeof(SmartSoftwareApplicationManager));
                builder.ReplaceAuthorizationManager(typeof(SmartSoftwareAuthorizationManager));
                builder.ReplaceScopeManager(typeof(SmartSoftwareScopeManager));
                builder.ReplaceTokenManager(typeof(SmartSoftwareTokenManager));

                builder.Services.TryAddScoped(provider => (ISmartSoftwareApplicationManager)provider.GetRequiredService<IOpenIddictApplicationManager>());

                services.ExecutePreConfiguredActions(builder);
            });

        services.ExecutePreConfiguredActions(openIddictBuilder);
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                OpenIddictModuleExtensionConsts.ModuleName,
                OpenIddictModuleExtensionConsts.EntityNames.Application,
                typeof(OpenIddictApplication)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                OpenIddictModuleExtensionConsts.ModuleName,
                OpenIddictModuleExtensionConsts.EntityNames.Application,
                typeof(OpenIddictApplicationModel)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                OpenIddictModuleExtensionConsts.ModuleName,
                OpenIddictModuleExtensionConsts.EntityNames.Authorization,
                typeof(OpenIddictAuthorization)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                OpenIddictModuleExtensionConsts.ModuleName,
                OpenIddictModuleExtensionConsts.EntityNames.Authorization,
                typeof(OpenIddictAuthorizationModel)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                OpenIddictModuleExtensionConsts.ModuleName,
                OpenIddictModuleExtensionConsts.EntityNames.Scope,
                typeof(OpenIddictScope)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                OpenIddictModuleExtensionConsts.ModuleName,
                OpenIddictModuleExtensionConsts.EntityNames.Scope,
                typeof(OpenIddictScopeModel)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                OpenIddictModuleExtensionConsts.ModuleName,
                OpenIddictModuleExtensionConsts.EntityNames.Token,
                typeof(OpenIddictToken)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                OpenIddictModuleExtensionConsts.ModuleName,
                OpenIddictModuleExtensionConsts.EntityNames.Token,
                typeof(OpenIddictTokenModel)
            );
        });
    }
}
