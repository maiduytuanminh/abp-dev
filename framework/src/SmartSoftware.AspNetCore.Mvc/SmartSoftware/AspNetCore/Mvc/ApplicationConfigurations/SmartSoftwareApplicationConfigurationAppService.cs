﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SmartSoftware.Application.Services;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;
using SmartSoftware.AspNetCore.Mvc.MultiTenancy;
using SmartSoftware.Authorization;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Data;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Localization;
using SmartSoftware.Localization.External;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Settings;
using SmartSoftware.Timing;
using SmartSoftware.Users;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public class SmartSoftwareApplicationConfigurationAppService : ApplicationService, ISmartSoftwareApplicationConfigurationAppService
{
    private readonly SmartSoftwareLocalizationOptions _localizationOptions;
    private readonly SmartSoftwareMultiTenancyOptions _multiTenancyOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly ISmartSoftwareAuthorizationPolicyProvider _ssAuthorizationPolicyProvider;
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;
    private readonly DefaultAuthorizationPolicyProvider _defaultAuthorizationPolicyProvider;
    private readonly IPermissionChecker _permissionChecker;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUser _currentUser;
    private readonly ISettingProvider _settingProvider;
    private readonly ISettingDefinitionManager _settingDefinitionManager;
    private readonly IFeatureDefinitionManager _featureDefinitionManager;
    private readonly ILanguageProvider _languageProvider;
    private readonly ITimezoneProvider _timezoneProvider;
    private readonly SmartSoftwareClockOptions _ssClockOptions;
    private readonly ICachedObjectExtensionsDtoService _cachedObjectExtensionsDtoService;
    private readonly SmartSoftwareApplicationConfigurationOptions _options;

    public SmartSoftwareApplicationConfigurationAppService(
        IOptions<SmartSoftwareLocalizationOptions> localizationOptions,
        IOptions<SmartSoftwareMultiTenancyOptions> multiTenancyOptions,
        IServiceProvider serviceProvider,
        ISmartSoftwareAuthorizationPolicyProvider ssAuthorizationPolicyProvider,
        IPermissionDefinitionManager permissionDefinitionManager,
        DefaultAuthorizationPolicyProvider defaultAuthorizationPolicyProvider,
        IPermissionChecker permissionChecker,
        IAuthorizationService authorizationService,
        ICurrentUser currentUser,
        ISettingProvider settingProvider,
        ISettingDefinitionManager settingDefinitionManager,
        IFeatureDefinitionManager featureDefinitionManager,
        ILanguageProvider languageProvider,
        ITimezoneProvider timezoneProvider,
        IOptions<SmartSoftwareClockOptions> ssClockOptions,
        ICachedObjectExtensionsDtoService cachedObjectExtensionsDtoService,
        IOptions<SmartSoftwareApplicationConfigurationOptions> options)
    {
        _serviceProvider = serviceProvider;
        _ssAuthorizationPolicyProvider = ssAuthorizationPolicyProvider;
        _permissionDefinitionManager = permissionDefinitionManager;
        _defaultAuthorizationPolicyProvider = defaultAuthorizationPolicyProvider;
        _permissionChecker = permissionChecker;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
        _settingProvider = settingProvider;
        _settingDefinitionManager = settingDefinitionManager;
        _featureDefinitionManager = featureDefinitionManager;
        _languageProvider = languageProvider;
        _timezoneProvider = timezoneProvider;
        _ssClockOptions = ssClockOptions.Value;
        _cachedObjectExtensionsDtoService = cachedObjectExtensionsDtoService;
        _options = options.Value;
        _localizationOptions = localizationOptions.Value;
        _multiTenancyOptions = multiTenancyOptions.Value;
    }

    public virtual async Task<ApplicationConfigurationDto> GetAsync(ApplicationConfigurationRequestOptions options)
    {
        //TODO: Optimize & cache..?

        Logger.LogDebug("Executing SmartSoftwareApplicationConfigurationAppService.GetAsync()...");

        var result = new ApplicationConfigurationDto
        {
            Auth = await GetAuthConfigAsync(),
            Features = await GetFeaturesConfigAsync(),
            GlobalFeatures = await GetGlobalFeaturesConfigAsync(),
            Localization = await GetLocalizationConfigAsync(options),
            CurrentUser = GetCurrentUser(),
            Setting = await GetSettingConfigAsync(),
            MultiTenancy = GetMultiTenancy(),
            CurrentTenant = GetCurrentTenant(),
            Timing = await GetTimingConfigAsync(),
            Clock = GetClockConfig(),
            ObjectExtensions = _cachedObjectExtensionsDtoService.Get(),
            ExtraProperties = new ExtraPropertyDictionary()
        };

        if (_options.Contributors.Any())
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = new ApplicationConfigurationContributorContext(scope.ServiceProvider, result);
                foreach (var contributor in _options.Contributors)
                {
                    await contributor.ContributeAsync(context);
                }
            }
        }

        Logger.LogDebug("Executed SmartSoftwareApplicationConfigurationAppService.GetAsync().");

        return result;
    }

    protected virtual CurrentTenantDto GetCurrentTenant()
    {
        return new CurrentTenantDto()
        {
            Id = CurrentTenant.Id,
            Name = CurrentTenant.Name!,
            IsAvailable = CurrentTenant.IsAvailable
        };
    }

    protected virtual MultiTenancyInfoDto GetMultiTenancy()
    {
        return new MultiTenancyInfoDto
        {
            IsEnabled = _multiTenancyOptions.IsEnabled
        };
    }

    protected virtual CurrentUserDto GetCurrentUser()
    {
        return new CurrentUserDto
        {
            IsAuthenticated = _currentUser.IsAuthenticated,
            Id = _currentUser.Id,
            TenantId = _currentUser.TenantId,
            ImpersonatorUserId = _currentUser.FindImpersonatorUserId(),
            ImpersonatorTenantId = _currentUser.FindImpersonatorTenantId(),
            ImpersonatorUserName = _currentUser.FindImpersonatorUserName(),
            ImpersonatorTenantName = _currentUser.FindImpersonatorTenantName(),
            UserName = _currentUser.UserName,
            SurName = _currentUser.SurName,
            Name = _currentUser.Name,
            Email = _currentUser.Email,
            EmailVerified = _currentUser.EmailVerified,
            PhoneNumber = _currentUser.PhoneNumber,
            PhoneNumberVerified = _currentUser.PhoneNumberVerified,
            Roles = _currentUser.Roles,
            SessionId = _currentUser.FindSessionId()
        };
    }

    protected virtual async Task<ApplicationAuthConfigurationDto> GetAuthConfigAsync()
    {
        var authConfig = new ApplicationAuthConfigurationDto();

        var policyNames = await _ssAuthorizationPolicyProvider.GetPoliciesNamesAsync();
        var ssPolicyNames = new List<string>();
        var otherPolicyNames = new List<string>();

        foreach (var policyName in policyNames)
        {
            if (await _defaultAuthorizationPolicyProvider.GetPolicyAsync(policyName) == null &&
                await _permissionDefinitionManager.GetOrNullAsync(policyName) != null)
            {
                ssPolicyNames.Add(policyName);
            }
            else
            {
                otherPolicyNames.Add(policyName);
            }
        }

        foreach (var policyName in otherPolicyNames)
        {
            if (await _authorizationService.IsGrantedAsync(policyName))
            {
                authConfig.GrantedPolicies[policyName] = true;
            }
        }

        var result = await _permissionChecker.IsGrantedAsync(ssPolicyNames.ToArray());
        foreach (var (key, value) in result.Result)
        {
            if (value == PermissionGrantResult.Granted)
            {
                authConfig.GrantedPolicies[key] = true;
            }
        }

        return authConfig;
    }

    protected virtual async Task<ApplicationLocalizationConfigurationDto> GetLocalizationConfigAsync(
        ApplicationConfigurationRequestOptions options)
    {
        var localizationConfig = new ApplicationLocalizationConfigurationDto();

        localizationConfig.Languages.AddRange(await _languageProvider.GetLanguagesAsync());

        if (options.IncludeLocalizationResources)
        {
            var resourceNames = _localizationOptions
                .Resources
                .Values
                .Select(x => x.ResourceName)
                .Union(
                    await LazyServiceProvider
                        .LazyGetRequiredService<IExternalLocalizationStore>()
                        .GetResourceNamesAsync()
                );

            foreach (var resourceName in resourceNames)
            {
                var dictionary = new Dictionary<string, string>();

                var localizer = await StringLocalizerFactory
                    .CreateByResourceNameOrNullAsync(resourceName);

                if (localizer != null)
                {
                    foreach (var localizedString in await localizer.GetAllStringsAsync())
                    {
                        dictionary[localizedString.Name] = localizedString.Value;
                    }
                }

                localizationConfig.Values[resourceName] = dictionary;
            }
        }

        localizationConfig.CurrentCulture = GetCurrentCultureInfo();

        if (_localizationOptions.DefaultResourceType != null)
        {
            localizationConfig.DefaultResourceName = LocalizationResourceNameAttribute.GetName(
                _localizationOptions.DefaultResourceType
            );
        }

        localizationConfig.LanguagesMap = _localizationOptions.LanguagesMap;
        localizationConfig.LanguageFilesMap = _localizationOptions.LanguageFilesMap;

        return localizationConfig;
    }

    private static CurrentCultureDto GetCurrentCultureInfo()
    {
        return CurrentCultureDto.Create();
    }

    private async Task<ApplicationSettingConfigurationDto> GetSettingConfigAsync()
    {
        var result = new ApplicationSettingConfigurationDto
        {
            Values = new Dictionary<string, string?>()
        };

        var settingDefinitions = (await _settingDefinitionManager.GetAllAsync()).Where(x => x.IsVisibleToClients);

        var settingValues = await _settingProvider.GetAllAsync(settingDefinitions.Select(x => x.Name).ToArray());

        foreach (var settingValue in settingValues)
        {
            result.Values[settingValue.Name] = settingValue.Value;
        }

        return result;
    }

    protected virtual async Task<ApplicationFeatureConfigurationDto> GetFeaturesConfigAsync()
    {
        var result = new ApplicationFeatureConfigurationDto();

        foreach (var featureDefinition in await _featureDefinitionManager.GetAllAsync())
        {
            if (!featureDefinition.IsVisibleToClients)
            {
                continue;
            }

            result.Values[featureDefinition.Name] = await FeatureChecker.GetOrNullAsync(featureDefinition.Name);
        }

        return result;
    }

    protected virtual Task<ApplicationGlobalFeatureConfigurationDto> GetGlobalFeaturesConfigAsync()
    {
        var result = new ApplicationGlobalFeatureConfigurationDto();

        foreach (var enabledFeatureName in GlobalFeatureManager.Instance.GetEnabledFeatureNames())
        {
            result.EnabledFeatures.AddIfNotContains(enabledFeatureName);
        }

        return Task.FromResult(result);
    }

    protected virtual async Task<TimingDto> GetTimingConfigAsync()
    {
        var windowsTimeZoneId = await _settingProvider.GetOrNullAsync(TimingSettingNames.TimeZone);

        return new TimingDto
        {
            TimeZone = new TimeZone
            {
                Windows = new WindowsTimeZone
                {
                    TimeZoneId = windowsTimeZoneId
                },
                Iana = new IanaTimeZone
                {
                    TimeZoneName = windowsTimeZoneId.IsNullOrWhiteSpace()
                        ? null
                        : _timezoneProvider.WindowsToIana(windowsTimeZoneId!)
                }
            }
        };
    }

    protected virtual ClockDto GetClockConfig()
    {
        return new ClockDto
        {
            Kind = Enum.GetName(typeof(DateTimeKind), _ssClockOptions.Kind)!
        };
    }
}