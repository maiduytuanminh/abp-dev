using SmartSoftware.Collections;

namespace SmartSoftware.EventBus.Local;

public class SmartSoftwareLocalEventBusOptions
{
    public ITypeList<IEventHandler> Handlers { get; }

    public SmartSoftwareLocalEventBusOptions()
    {
        Handlers = new TypeList<IEventHandler>();
    }
}
