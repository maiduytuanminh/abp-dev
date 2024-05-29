namespace SmartSoftware.Domain.Entities.Events.Distributed;

public class SmartSoftwareDistributedEntityEventOptions
{
    public IAutoEntityDistributedEventSelectorList AutoEventSelectors { get; }

    public EtoMappingDictionary EtoMappings { get; set; }

    public SmartSoftwareDistributedEntityEventOptions()
    {
        AutoEventSelectors = new AutoEntityDistributedEventSelectorList();
        EtoMappings = new EtoMappingDictionary();
    }
}
