using SmartSoftware.Localization;
using SmartSoftware.Validation.StringValues;

namespace SmartSoftware.Features;

public interface ICanCreateChildFeature
{
    FeatureDefinition CreateChildFeature(
        string name,
        string? defaultValue = null,
        ILocalizableString? displayName = null,
        ILocalizableString? description = null,
        IStringValueType? valueType = null,
        bool isVisibleToClients = true,
        bool isAvailableToHost = true);
}
