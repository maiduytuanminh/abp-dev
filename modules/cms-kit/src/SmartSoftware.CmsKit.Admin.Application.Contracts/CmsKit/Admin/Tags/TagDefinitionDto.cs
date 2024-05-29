using System;

namespace SmartSoftware.CmsKit.Admin.Tags;

[Serializable]
public class TagDefinitionDto
{
    public string EntityType { get; set; }

    public string DisplayName { get; set; }
}
