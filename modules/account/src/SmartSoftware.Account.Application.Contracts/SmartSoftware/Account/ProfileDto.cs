using SmartSoftware.Domain.Entities;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.Account;

public class ProfileDto : ExtensibleObject, IHasConcurrencyStamp
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string PhoneNumber { get; set; }

    public bool IsExternal { get; set; }

    public bool HasPassword { get; set; }

    public string ConcurrencyStamp { get; set; }
}
