namespace SmartSoftware.Auditing;

public interface IAuditSerializer
{
    string Serialize(object obj);
}
