namespace SmartSoftware.Domain.Entities.Events;

public class SmartSoftwareEntityChangeOptions
{
    /// <summary>
    /// Default: true.
    /// Publish the EntityUpdatedEvent when any navigation property changes.
    /// </summary>
    public bool PublishEntityUpdatedEventWhenNavigationChanges { get; set; } = true;
}
