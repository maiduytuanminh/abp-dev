using System;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Account;

public class AccountTestData : ISingletonDependency
{
    public Guid UserJohnId { get; } = Guid.NewGuid();
}
