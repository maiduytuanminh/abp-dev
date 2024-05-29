using System;
using SmartSoftware.CmsKit.Contents;

namespace SmartSoftware.CmsKit.Admin.Contents;

[Serializable]
public class ContentWidgetDto
{
    public string Key { get; set; }
    
    public WidgetDetailDto Details { get; set; }
}