using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Admin.Menus;

[Serializable]
public class PageLookupDto : EntityDto<Guid>
{
    public string Title { get; set; }

    public string Slug { get; set; }
}
