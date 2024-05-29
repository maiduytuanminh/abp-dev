using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSoftware.Features;

public interface IDynamicFeatureDefinitionStore
{
    Task<FeatureDefinition?> GetOrNullAsync(string name);

    Task<IReadOnlyList<FeatureDefinition>> GetFeaturesAsync();

    Task<IReadOnlyList<FeatureGroupDefinition>> GetGroupsAsync();
}
