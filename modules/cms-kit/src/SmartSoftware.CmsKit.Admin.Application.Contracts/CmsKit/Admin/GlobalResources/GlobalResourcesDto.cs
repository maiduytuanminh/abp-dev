using System;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.CmsKit.Admin.GlobalResources;

[Serializable]
public class GlobalResourcesDto : ExtensibleObject
{
    public string StyleContent { get; set; }
    
    public string ScriptContent { get; set; }
}