namespace SmartSoftware.DistributedLocking;

public interface IDistributedLockKeyNormalizer
{
    string NormalizeKey(string name);
    
}