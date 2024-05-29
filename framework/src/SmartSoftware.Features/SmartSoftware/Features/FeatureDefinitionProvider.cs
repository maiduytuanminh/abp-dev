using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Features;

public abstract class FeatureDefinitionProvider : IFeatureDefinitionProvider, ITransientDependency
{
    public abstract void Define(IFeatureDefinitionContext context);
}
