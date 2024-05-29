using SmartSoftware.Localization;

namespace SmartSoftware.ExceptionHandling;

public interface ILocalizeErrorMessage
{
    string LocalizeMessage(LocalizationContext context);
}
