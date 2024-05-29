using Microsoft.Extensions.Localization;

namespace SmartSoftware.Localization;

public interface ITemplateLocalizer
{
    string Localize(IStringLocalizer localizer, string text);
}
