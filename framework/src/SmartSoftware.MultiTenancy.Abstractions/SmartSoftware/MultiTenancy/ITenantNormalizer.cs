namespace SmartSoftware.MultiTenancy;

public interface ITenantNormalizer
{
    string? NormalizeName(string? name);
}
