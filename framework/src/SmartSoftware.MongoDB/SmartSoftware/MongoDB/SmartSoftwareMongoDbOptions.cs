using SmartSoftware.Timing;

namespace SmartSoftware.MongoDB;

public class SmartSoftwareMongoDbOptions
{
    /// <summary>
    /// Serializer the datetime based on <see cref="SmartSoftwareClockOptions.Kind"/> in MongoDb.
    /// Default: true.
    /// </summary>
    public bool UseSmartSoftwareClockHandleDateTime { get; set; }

    public SmartSoftwareMongoDbOptions()
    {
        UseSmartSoftwareClockHandleDateTime = true;
    }
}
