using System;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.CmsKit.Admin.GlobalResources;

[Serializable]
public class GlobalResourcesUpdateDto : ExtensibleObject
{
    public string Style { get; set; }
    
    public string Script { get; set; }
}