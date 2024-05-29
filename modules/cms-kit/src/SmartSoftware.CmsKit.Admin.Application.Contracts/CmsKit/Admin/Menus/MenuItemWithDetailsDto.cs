using System;
using SmartSoftware.CmsKit.Menus;

namespace SmartSoftware.CmsKit.Admin.Menus;

[Serializable]
public class MenuItemWithDetailsDto : MenuItemDto
{
    public string? PageTitle { get; set; }
}
