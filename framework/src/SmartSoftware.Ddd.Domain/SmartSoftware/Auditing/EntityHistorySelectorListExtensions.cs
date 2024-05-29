using System.Linq;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Auditing;

public static class EntityHistorySelectorListExtensions
{
    public const string AllEntitiesSelectorName = "SmartSoftware.Entities.All";

    public static void AddAllEntities(this IEntityHistorySelectorList selectors)
    {
        if (selectors.Any(s => s.Name == AllEntitiesSelectorName))
        {
            return;
        }

        selectors.Add(new NamedTypeSelector(AllEntitiesSelectorName, t => typeof(IEntity).IsAssignableFrom(t)));
    }
}
