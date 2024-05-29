using System.Collections.Generic;
using SmartSoftware.Collections;

namespace SmartSoftware.Settings;

public class SmartSoftwareSettingOptions
{
    public ITypeList<ISettingDefinitionProvider> DefinitionProviders { get; }

    public ITypeList<ISettingValueProvider> ValueProviders { get; }

    public HashSet<string> DeletedSettings { get; }

    public SmartSoftwareSettingOptions()
    {
        DefinitionProviders = new TypeList<ISettingDefinitionProvider>();
        ValueProviders = new TypeList<ISettingValueProvider>();
        DeletedSettings = new HashSet<string>();
    }
}
