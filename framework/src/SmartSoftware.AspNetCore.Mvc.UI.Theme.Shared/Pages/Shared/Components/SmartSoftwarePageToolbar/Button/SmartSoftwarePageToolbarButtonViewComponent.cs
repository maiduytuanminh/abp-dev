using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;
using SmartSoftware.Localization;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.SmartSoftwarePageToolbar.Button;

public class SmartSoftwarePageToolbarButtonViewComponent : SmartSoftwareViewComponent
{
    protected IStringLocalizerFactory StringLocalizerFactory { get; }

    public SmartSoftwarePageToolbarButtonViewComponent(IStringLocalizerFactory stringLocalizerFactory)
    {
        StringLocalizerFactory = stringLocalizerFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(
        ILocalizableString text,
        string name,
        string icon,
        string id,
        ILocalizableString? busyText,
        FontIconType iconType,
        SmartSoftwareButtonType type,
        SmartSoftwareButtonSize size,
        bool disabled)
    {
        Check.NotNull(text, nameof(text));

        return View(
            "~/Pages/Shared/Components/SmartSoftwarePageToolbar/Button/Default.cshtml",
            new SmartSoftwarePageToolbarButtonViewModel(
                await text.LocalizeAsync(StringLocalizerFactory),
                name,
                icon,
                id,
                busyText == null ? null : (await busyText.LocalizeAsync(StringLocalizerFactory)).ToString(),
                iconType,
                type,
                size,
                disabled
            )
        );
    }

    public class SmartSoftwarePageToolbarButtonViewModel
    {
        public string Text { get; }
        public string Name { get; }
        public string Icon { get; }
        public string Id { get; }
        public string? BusyText { get; }
        public FontIconType IconType { get; }
        public SmartSoftwareButtonType Type { get; }
        public SmartSoftwareButtonSize Size { get; }
        public bool Disabled { get; }

        public SmartSoftwarePageToolbarButtonViewModel(
            string text,
            string name,
            string icon,
            string id,
            string? busyText,
            FontIconType iconType,
            SmartSoftwareButtonType type,
            SmartSoftwareButtonSize size,
            bool disabled)
        {
            Text = text;
            Name = name;
            Icon = icon;
            Id = id;
            BusyText = busyText;
            IconType = iconType;
            Type = type;
            Size = size;
            Disabled = disabled;
        }
    }
}
