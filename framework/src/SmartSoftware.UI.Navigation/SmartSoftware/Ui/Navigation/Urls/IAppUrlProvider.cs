﻿using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SmartSoftware.UI.Navigation.Urls;

public interface IAppUrlProvider
{
    Task<string> GetUrlAsync([NotNull] string appName, string? urlName = null);

    Task<string?> GetUrlOrNullAsync([NotNull] string appName, string? urlName = null);

    Task<bool> IsRedirectAllowedUrlAsync(string url);

    Task<string?> NormalizeUrlAsync(string? url);
}
