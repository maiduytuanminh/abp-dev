using System;
using System.ComponentModel.DataAnnotations.Schema;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.EntityFrameworkCore.TestApp.SecondContext;

[Table("AppPhones")]
public class PhoneInSecondDbContext : AggregateRoot
{
    public virtual Guid PersonId { get; set; }

    public virtual string Number { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { PersonId, Number };
    }
}
