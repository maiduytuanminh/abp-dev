using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SmartSoftware.Settings;

public interface IDynamicSettingDefinitionStore
{
    Task<SettingDefinition> GetAsync([NotNull] string name);

    Task<IReadOnlyList<SettingDefinition>> GetAllAsync();

    Task<SettingDefinition?> GetOrNullAsync([NotNull] string name);
}
