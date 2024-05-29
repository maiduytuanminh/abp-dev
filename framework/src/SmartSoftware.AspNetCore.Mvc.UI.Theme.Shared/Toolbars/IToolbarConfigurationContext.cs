﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Theming;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

public interface IToolbarConfigurationContext : IServiceProviderAccessor
{
    ITheme Theme { get; }

    Toolbar Toolbar { get; }

    IAuthorizationService AuthorizationService { get; }

    IStringLocalizerFactory StringLocalizerFactory { get; }

    Task<bool> IsGrantedAsync(string policyName);

    IStringLocalizer? GetDefaultLocalizer();

    [NotNull]
    public IStringLocalizer GetLocalizer<T>();

    [NotNull]
    public IStringLocalizer GetLocalizer(Type resourceType);
}
