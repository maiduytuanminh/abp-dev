﻿using SmartSoftware.Localization;

namespace SmartSoftware.AspNetCore.Components.Notifications;

/// <summary>
/// Options to override notification appearance.
/// </summary>
public class UiNotificationOptions
{
    /// <summary>
    /// Custom text for the Ok button.
    /// </summary>
    public ILocalizableString? OkButtonText { get; set; }

    /// <summary>
    /// Custom icon for the Ok button.
    /// </summary>
    public object? OkButtonIcon { get; set; }
}
