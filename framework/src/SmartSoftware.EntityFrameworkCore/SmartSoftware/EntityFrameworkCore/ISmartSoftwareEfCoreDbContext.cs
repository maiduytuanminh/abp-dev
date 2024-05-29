namespace SmartSoftware.EntityFrameworkCore;

public interface ISmartSoftwareEfCoreDbContext : IEfCoreDbContext
{
    void Initialize(SmartSoftwareEfCoreDbContextInitializationContext initializationContext);
}
