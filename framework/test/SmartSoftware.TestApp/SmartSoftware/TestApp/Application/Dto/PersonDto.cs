using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Application.Dtos;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.TestApp.Application.Dto;

public class PersonDto : EntityDto<Guid>, IMultiTenant
{
    [Required]
    public string Name { get; set; }

    public int Age { get; set; }

    public Guid? TenantId { get; set; }
}
