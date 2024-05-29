using SmartSoftware.Validation.StringValues;

namespace SmartSoftware.FeatureManagement.JsonConverters;

public interface IValueValidatorFactory
{
    bool CanCreate(string name);

    IValueValidator Create();
}
