using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Settings;

public abstract class SettingDefinitionProvider : ISettingDefinitionProvider, ITransientDependency
{
    public abstract void Define(ISettingDefinitionContext context);
}
