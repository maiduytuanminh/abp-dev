using System.Threading.Tasks;
using Microsoft.JSInterop;
using SmartSoftware.AspNetCore.Components.Web.Security;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using SmartSoftware.AspNetCore.Mvc.Client;
using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.AspNetCore.Components.WebAssembly;

public class WebAssemblyCachedApplicationConfigurationClient : ICachedApplicationConfigurationClient, ITransientDependency
{
    protected SmartSoftwareApplicationConfigurationClientProxy ApplicationConfigurationClientProxy { get; }

    protected SmartSoftwareApplicationLocalizationClientProxy ApplicationLocalizationClientProxy { get; }

    protected ApplicationConfigurationCache Cache { get; }

    protected ICurrentTenantAccessor CurrentTenantAccessor { get; }

    protected ApplicationConfigurationChangedService ApplicationConfigurationChangedService { get; }

    protected IJSRuntime JSRuntime { get; }

    public WebAssemblyCachedApplicationConfigurationClient(
        SmartSoftwareApplicationConfigurationClientProxy applicationConfigurationClientProxy,
        ApplicationConfigurationCache cache,
        ICurrentTenantAccessor currentTenantAccessor,
        SmartSoftwareApplicationLocalizationClientProxy applicationLocalizationClientProxy,
        ApplicationConfigurationChangedService applicationConfigurationChangedService,
        IJSRuntime jsRuntime)
    {
        ApplicationConfigurationClientProxy = applicationConfigurationClientProxy;
        Cache = cache;
        CurrentTenantAccessor = currentTenantAccessor;
        ApplicationLocalizationClientProxy = applicationLocalizationClientProxy;
        ApplicationConfigurationChangedService = applicationConfigurationChangedService;
        JSRuntime = jsRuntime;
    }

    public virtual async Task InitializeAsync()
    {
        var configurationDto = await ApplicationConfigurationClientProxy.GetAsync(
            new ApplicationConfigurationRequestOptions {
                IncludeLocalizationResources = false
            }
        );

        var localizationDto = await ApplicationLocalizationClientProxy.GetAsync(
            new ApplicationLocalizationRequestDto {
                CultureName = configurationDto.Localization.CurrentCulture.Name,
                OnlyDynamics = true
            }
        );

        configurationDto.Localization.Resources = localizationDto.Resources;

        Cache.Set(configurationDto);

        if (!configurationDto.CurrentUser.IsAuthenticated)
        {
            await JSRuntime.InvokeVoidAsync("ss.utils.removeOidcUser");
        }

        ApplicationConfigurationChangedService.NotifyChanged();

        CurrentTenantAccessor.Current = new BasicTenantInfo(
            configurationDto.CurrentTenant.Id,
            configurationDto.CurrentTenant.Name
        );
    }

    public virtual Task<ApplicationConfigurationDto> GetAsync()
    {
        return Task.FromResult(GetConfigurationByChecking());
    }

    public virtual ApplicationConfigurationDto Get()
    {
        return GetConfigurationByChecking();
    }

    private ApplicationConfigurationDto GetConfigurationByChecking()
    {
        var configuration = Cache.Get();
        if (configuration == null)
        {
            throw new SmartSoftwareException(
                $"{nameof(WebAssemblyCachedApplicationConfigurationClient)} should be initialized before using it.");
        }

        return configuration;
    }
}
