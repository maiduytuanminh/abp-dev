using System;
using JetBrains.Annotations;
using SmartSoftware.Data;
using SmartSoftware.Domain.Entities;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Users;

public interface IUser : IAggregateRoot<Guid>, IMultiTenant, IHasExtraProperties
{
    string UserName { get; }

    [CanBeNull]
    string Email { get; }

    [CanBeNull]
    string Name { get; }

    [CanBeNull]
    string Surname { get; }

    bool IsActive { get; }

    bool EmailConfirmed { get; }

    [CanBeNull]
    string PhoneNumber { get; }

    bool PhoneNumberConfirmed { get; }
}
