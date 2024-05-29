namespace SmartSoftware.EntityFrameworkCore.DependencyInjection;

public interface ISmartSoftwareDbContextConfigurer
{
    void Configure(SmartSoftwareDbContextConfigurationContext context);
}

public interface ISmartSoftwareDbContextConfigurer<TDbContext>
    where TDbContext : SmartSoftwareDbContext<TDbContext>
{
    void Configure(SmartSoftwareDbContextConfigurationContext<TDbContext> context);
}
