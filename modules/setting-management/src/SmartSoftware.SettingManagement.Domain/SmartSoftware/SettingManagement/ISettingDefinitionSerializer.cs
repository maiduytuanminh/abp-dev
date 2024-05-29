using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Settings;

namespace SmartSoftware.SettingManagement;

public interface ISettingDefinitionSerializer
{
    Task<SettingDefinitionRecord> SerializeAsync(SettingDefinition setting);

    Task<List<SettingDefinitionRecord>> SerializeAsync(IEnumerable<SettingDefinition> settings);
}
