using System;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.CmsKit.Public.Comments;

[Serializable]
public class CmsUserDto : ExtensibleObject
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }
}
