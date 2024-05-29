﻿using System.ComponentModel.DataAnnotations;
using SmartSoftware.Auditing;

namespace SmartSoftware.TenantManagement;

public class TenantCreateDto : TenantCreateOrUpdateDtoBase
{
    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public virtual string AdminEmailAddress { get; set; }

    [Required]
    [MaxLength(128)]
    [DisableAuditing]
    public virtual string AdminPassword { get; set; }
}
