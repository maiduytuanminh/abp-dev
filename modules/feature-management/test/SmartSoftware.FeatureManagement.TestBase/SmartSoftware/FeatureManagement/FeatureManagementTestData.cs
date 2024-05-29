using System;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.FeatureManagement;

public class FeatureManagementTestData : ISingletonDependency
{
    public Guid User1Id { get; } = Guid.NewGuid();
}
