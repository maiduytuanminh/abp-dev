using System;
using JetBrains.Annotations;
using SmartSoftware.Data;

namespace SmartSoftware.Users;

public interface IUserData : IHasExtraProperties
{
    Guid Id { get; }

    Guid? TenantId { get; }

    string UserName { get; }

    string Name { get; }

    string Surname { get; }

    bool IsActive { get; }

    [CanBeNull]
    string Email { get; }

    bool EmailConfirmed { get; }

    [CanBeNull]
    string PhoneNumber { get; }

    bool PhoneNumberConfirmed { get; }
}
