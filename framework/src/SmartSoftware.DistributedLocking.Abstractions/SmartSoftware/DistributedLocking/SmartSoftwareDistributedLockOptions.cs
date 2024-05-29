namespace SmartSoftware.DistributedLocking;

public class SmartSoftwareDistributedLockOptions
{
    /// <summary>
    /// DistributedLock key prefix.
    /// </summary>
    public string KeyPrefix  { get; set; }
    
    public SmartSoftwareDistributedLockOptions()
    {
        KeyPrefix = "";
    }
}