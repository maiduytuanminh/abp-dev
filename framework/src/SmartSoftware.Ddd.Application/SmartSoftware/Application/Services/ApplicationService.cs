using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Aspects;
using SmartSoftware.Auditing;
using SmartSoftware.Authorization;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Guids;
using SmartSoftware.Linq;
using SmartSoftware.Localization;
using SmartSoftware.MultiTenancy;
using SmartSoftware.ObjectMapping;
using SmartSoftware.Settings;
using SmartSoftware.Timing;
using SmartSoftware.Uow;
using SmartSoftware.Users;
using SmartSoftware.Validation;

namespace SmartSoftware.Application.Services;

public abstract class ApplicationService :
    IApplicationService,
    IAvoidDuplicateCrossCuttingConcerns,
    IValidationEnabled,
    IUnitOfWorkEnabled,
    IAuditingEnabled,
    IGlobalFeatureCheckingEnabled,
    ITransientDependency
{
    public ISmartSoftwareLazyServiceProvider LazyServiceProvider { get; set; } = default!;

    [Obsolete("Use LazyServiceProvider instead.")]
    public IServiceProvider ServiceProvider { get; set; } = default!;

    public static string[] CommonPostfixes { get; set; } = { "AppService", "ApplicationService", "Service" };

    public List<string> AppliedCrossCuttingConcerns { get; } = new();

    protected IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();

    protected IAsyncQueryableExecuter AsyncExecuter => LazyServiceProvider.LazyGetRequiredService<IAsyncQueryableExecuter>();

    protected Type? ObjectMapperContext { get; set; }
    protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
        ObjectMapperContext == null
            ? provider.GetRequiredService<IObjectMapper>()
            : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));

    protected IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);

    protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

    protected ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();

    protected IDataFilter DataFilter => LazyServiceProvider.LazyGetRequiredService<IDataFilter>();

    protected ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();

    protected ISettingProvider SettingProvider => LazyServiceProvider.LazyGetRequiredService<ISettingProvider>();

    protected IClock Clock => LazyServiceProvider.LazyGetRequiredService<IClock>();

    protected IAuthorizationService AuthorizationService => LazyServiceProvider.LazyGetRequiredService<IAuthorizationService>();

    protected IFeatureChecker FeatureChecker => LazyServiceProvider.LazyGetRequiredService<IFeatureChecker>();

    protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

    protected IStringLocalizer L {
        get {
            if (_localizer == null)
            {
                _localizer = CreateLocalizer();
            }

            return _localizer;
        }
    }
    private IStringLocalizer? _localizer;

    protected Type? LocalizationResource {
        get => _localizationResource;
        set {
            _localizationResource = value;
            _localizer = null;
        }
    }
    private Type? _localizationResource = typeof(DefaultResource);

    protected IUnitOfWork? CurrentUnitOfWork => UnitOfWorkManager?.Current;

    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName!) ?? NullLogger.Instance);

    /// <summary>
    /// Checks for given <paramref name="policyName"/>.
    /// Throws <see cref="SmartSoftwareAuthorizationException"/> if given policy has not been granted.
    /// </summary>
    /// <param name="policyName">The policy name. This method does nothing if given <paramref name="policyName"/> is null or empty.</param>
    protected virtual async Task CheckPolicyAsync(string? policyName)
    {
        if (string.IsNullOrEmpty(policyName))
        {
            return;
        }

        await AuthorizationService.CheckAsync(policyName!);
    }

    protected virtual IStringLocalizer CreateLocalizer()
    {
        if (LocalizationResource != null)
        {
            return StringLocalizerFactory.Create(LocalizationResource);
        }

        var localizer = StringLocalizerFactory.CreateDefaultOrNull();
        if (localizer == null)
        {
            throw new SmartSoftwareException($"Set {nameof(LocalizationResource)} or define the default localization resource type (by configuring the {nameof(SmartSoftwareLocalizationOptions)}.{nameof(SmartSoftwareLocalizationOptions.DefaultResourceType)}) to be able to use the {nameof(L)} object!");
        }

        return localizer;
    }
}
