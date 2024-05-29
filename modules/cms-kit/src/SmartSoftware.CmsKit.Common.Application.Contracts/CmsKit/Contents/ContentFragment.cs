using System;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.CmsKit.Contents;

[Serializable]
public class ContentFragment : ExtensibleObject
{
    public string Type { get; set; }
}
