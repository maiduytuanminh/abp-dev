using JetBrains.Annotations;

namespace SmartSoftware.Domain.Entities.Events.Distributed;

public interface IEntityToEtoMapper
{
    object? Map(object entityObj);
}
