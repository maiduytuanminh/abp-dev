using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.TenantManagement;

public class TenantDto : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; }

    public string ConcurrencyStamp { get; set; }
}
