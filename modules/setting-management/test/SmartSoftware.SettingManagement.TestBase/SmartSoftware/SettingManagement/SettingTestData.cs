using System;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.SettingManagement;

public class SettingTestData : ISingletonDependency
{
    public Guid User1Id { get; } = Guid.NewGuid();
    public Guid User2Id { get; } = Guid.NewGuid();

    public Guid SettingId { get; } = Guid.NewGuid();
}
