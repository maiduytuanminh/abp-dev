using System.Collections.Generic;

namespace SmartSoftware.Settings;

public interface ISettingValueProviderManager
{
    List<ISettingValueProvider> Providers { get; }
}
