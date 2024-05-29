using SmartSoftware.Collections;

namespace SmartSoftware.Modularity;

public class SmartSoftwareModuleLifecycleOptions
{
    public ITypeList<IModuleLifecycleContributor> Contributors { get; }

    public SmartSoftwareModuleLifecycleOptions()
    {
        Contributors = new TypeList<IModuleLifecycleContributor>();
    }
}
