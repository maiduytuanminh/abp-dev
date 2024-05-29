using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.CmsKit.Menus;

[Serializable]
public class MenuItemDto : ExtensibleAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid? ParentId { get; set; }

    public string DisplayName { get; set; }

    public bool IsActive { get; set; }

    public string Url { get; set; }

    public string Icon { get; set; }

    public int Order { get; set; }

    public string Target { get; set; }

    public string ElementId { get; set; }

    public string CssClass { get; set; }

    public Guid? PageId { get; set; }

    public string ConcurrencyStamp { get; set; }
}
