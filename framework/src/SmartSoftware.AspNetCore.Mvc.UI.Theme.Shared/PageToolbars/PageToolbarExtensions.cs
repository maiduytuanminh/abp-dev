using System;
using Localization.Resources.SmartSoftwareUi;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.SmartSoftwarePageToolbar.Button;
using SmartSoftware.Localization;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public static class PageToolbarExtensions
{
    public static PageToolbar AddComponent<TComponent>(
        this PageToolbar toolbar,
        object? argument = null,
        int order = 0,
        string? requiredPolicyName = null)
    {
        return toolbar.AddComponent(
            typeof(TComponent),
            argument,
            order,
            requiredPolicyName
        );
    }

    public static PageToolbar AddComponent(
        this PageToolbar toolbar,
        Type componentType,
        object? argument = null,
        int order = 0,
        string? requiredPolicyName = null)
    {
        toolbar.Contributors.Add(
            new SimplePageToolbarContributor(
                componentType,
                argument,
                order,
                requiredPolicyName
            )
        );

        return toolbar;
    }

    public static PageToolbar AddButton(
        this PageToolbar toolbar,
        ILocalizableString text,
        string? icon = null,
        string? name = null,
        string? id = null,
        ILocalizableString? busyText = null,
        FontIconType iconType = FontIconType.FontAwesome,
        SmartSoftwareButtonType type = SmartSoftwareButtonType.Primary,
        SmartSoftwareButtonSize size = SmartSoftwareButtonSize.Small,
        bool disabled = false,
        int order = 0,
        string? requiredPolicyName = null)
    {
        if (busyText == null)
        {
            busyText = new LocalizableString(typeof(SmartSoftwareUiResource), "ProcessingWithThreeDot");
        }

        toolbar.AddComponent<SmartSoftwarePageToolbarButtonViewComponent>(
            new {
                text,
                icon,
                name,
                id,
                busyText,
                iconType,
                type,
                size,
                disabled
            },
            order,
            requiredPolicyName
        );

        return toolbar;
    }
}
