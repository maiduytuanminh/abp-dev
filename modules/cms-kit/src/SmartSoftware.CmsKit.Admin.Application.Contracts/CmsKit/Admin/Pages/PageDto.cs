using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.CmsKit.Admin.Pages;

[Serializable]
public class PageDto : ExtensibleAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Title { get; set; }

    public string Slug { get; set; }

    public string Content { get; set; }

    public string Script { get; set; }

    public string Style { get; set; }

    public bool IsHomePage { get; set; }

    public string ConcurrencyStamp { get; set; }
}
