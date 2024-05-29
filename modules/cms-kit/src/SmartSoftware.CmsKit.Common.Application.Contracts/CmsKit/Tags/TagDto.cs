using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.CmsKit.Tags;

[Serializable]
public class TagDto : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
{
    public string EntityType { get; set; }

    public string Name { get; set; }

    public string ConcurrencyStamp { get; set; }
}
