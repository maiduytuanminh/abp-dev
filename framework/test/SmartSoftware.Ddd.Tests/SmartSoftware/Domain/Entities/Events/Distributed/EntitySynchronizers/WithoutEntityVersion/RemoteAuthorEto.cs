using System;

namespace SmartSoftware.Domain.Entities.Events.Distributed.EntitySynchronizers.WithoutEntityVersion;

public class RemoteAuthorEto : EntityEto<Guid>
{
    public string Name { get; set; }
}