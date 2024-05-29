using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using SmartSoftware.AspNetCore.Mvc.Client;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Threading;

namespace SmartSoftware.Maui.Client;

public class MauiCachedApplicationConfigurationClient :
    ICachedApplicationConfigurationClient,
    ISingletonDependency
{
    protected SmartSoftwareApplicationConfigurationClientProxy ApplicationConfigurationClientProxy { get; }
    protected SmartSoftwareApplicationLocalizationClientProxy ApplicationLocalizationClientProxy { get; }
    protected ApplicationConfigurationCache Cache { get; }
    protected ICurrentTenantAccessor CurrentTenantAccessor { get; }

    public MauiCachedApplicationConfigurationClient(
        SmartSoftwareApplicationConfigurationClientProxy applicationConfigurationClientProxy,
        SmartSoftwareApplicationLocalizationClientProxy applicationLocalizationClientProxy,
        ApplicationConfigurationCache cache,
        ICurrentTenantAccessor currentTenantAccessor)
    {
        ApplicationConfigurationClientProxy = applicationConfigurationClientProxy;
        ApplicationLocalizationClientProxy = applicationLocalizationClientProxy;
        CurrentTenantAccessor = currentTenantAccessor;
        Cache = cache;
    }

    public virtual async Task<ApplicationConfigurationDto> InitializeAsync()
    {
        var configurationDto = await ApplicationConfigurationClientProxy.GetAsync(
            new ApplicationConfigurationRequestOptions
            {
                IncludeLocalizationResources = false,
            });

        var localizationDto = await ApplicationLocalizationClientProxy.GetAsync(
            new ApplicationLocalizationRequestDto
            {
                CultureName = configurationDto.Localization.CurrentCulture.Name,
                OnlyDynamics = true
            }
        );

        configurationDto.Localization.Resources = localizationDto.Resources;

        CurrentTenantAccessor.Current = new BasicTenantInfo(
            configurationDto.CurrentTenant.Id,
            configurationDto.CurrentTenant.Name);

        Cache.Set(configurationDto);

        return configurationDto;
    }

    public virtual ApplicationConfigurationDto Get()
    {
        return AsyncHelper.RunSync(GetAsync);
    }

    public virtual async Task<ApplicationConfigurationDto> GetAsync()
    {
        var configurationDto = Cache.Get();
        if (configurationDto is null)
        {
            return await InitializeAsync();
        }

        return configurationDto;
    }
}
