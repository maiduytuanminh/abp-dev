using System;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareIdentityServerTestData : ISingletonDependency
{
    public Guid Client1Id { get; } = Guid.NewGuid();

    public string Client1Name { get; } = "ClientId1";

    public Guid ApiResource1Id { get; } = Guid.NewGuid();

    public Guid IdentityResource1Id { get; } = Guid.NewGuid();
}
