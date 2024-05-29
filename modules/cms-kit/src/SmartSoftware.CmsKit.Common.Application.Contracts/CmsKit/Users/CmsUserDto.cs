using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Users;

[Serializable]
public class CmsUserDto : ExtensibleEntityDto<Guid>
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual string UserName { get; protected set; }

    public virtual string Name { get; set; }

    public virtual string Surname { get; set; }
}
