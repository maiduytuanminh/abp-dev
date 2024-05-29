using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Identity;

public class IdentityRoleDto : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; }

    public bool IsDefault { get; set; }

    public bool IsStatic { get; set; }

    public bool IsPublic { get; set; }

    public string ConcurrencyStamp { get; set; }
}
