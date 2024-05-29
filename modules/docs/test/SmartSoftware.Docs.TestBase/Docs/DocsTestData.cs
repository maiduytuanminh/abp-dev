using System;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Docs
{
    public class DocsTestData : ISingletonDependency
    {
        public Guid ProjectId { get; } = Guid.NewGuid();
    }
}
