using Microsoft.Extensions.Localization;

namespace SmartSoftware.Localization;

public interface ILocalizableString
{
    LocalizedString Localize(IStringLocalizerFactory stringLocalizerFactory);
}