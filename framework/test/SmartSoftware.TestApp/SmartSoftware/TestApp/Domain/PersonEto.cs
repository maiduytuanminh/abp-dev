using System;
using SmartSoftware.Domain.Entities.Events.Distributed;

namespace SmartSoftware.TestApp.Domain;

//[Serializable] //TODO: ???
public class PersonEto : EntityEto
{
    public virtual Guid? TenantId { get; set; }

    public virtual Guid? CityId { get; set; }

    public virtual string Name { get; set; }

    public virtual int Age { get; set; }
}
