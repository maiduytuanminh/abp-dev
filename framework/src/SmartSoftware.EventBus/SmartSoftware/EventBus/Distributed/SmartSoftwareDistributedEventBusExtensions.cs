namespace SmartSoftware.EventBus.Distributed;

public static class SmartSoftwareDistributedEventBusExtensions
{
    public static ISupportsEventBoxes AsSupportsEventBoxes(this IDistributedEventBus eventBus)
    {
        var supportsEventBoxes = eventBus as ISupportsEventBoxes;
        if (supportsEventBoxes == null)
        {
            throw new SmartSoftwareException($"Given type ({eventBus.GetType().AssemblyQualifiedName}) should implement {nameof(ISupportsEventBoxes)}!");
        }

        return supportsEventBoxes;
    }
}
