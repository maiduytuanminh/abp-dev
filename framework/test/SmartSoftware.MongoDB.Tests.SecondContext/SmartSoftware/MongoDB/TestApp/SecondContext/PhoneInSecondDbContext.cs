using System;
using System.ComponentModel.DataAnnotations.Schema;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.MongoDB.TestApp.SecondContext;

public class PhoneInSecondDbContext : AggregateRoot
{
    public virtual Guid PersonId { get; set; }

    public virtual string Number { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { PersonId, Number };
    }
}
