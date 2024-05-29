using System.Collections.Generic;

namespace SmartSoftware.Settings;

public interface ISettingDefinitionContext
{
    SettingDefinition? GetOrNull(string name);

    IReadOnlyList<SettingDefinition> GetAll();

    void Add(params SettingDefinition[] definitions);
}
