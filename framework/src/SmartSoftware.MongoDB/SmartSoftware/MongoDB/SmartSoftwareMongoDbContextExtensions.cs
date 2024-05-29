namespace SmartSoftware.MongoDB;

public static class SmartSoftwareMongoDbContextExtensions
{
    public static SmartSoftwareMongoDbContext ToSmartSoftwareMongoDbContext(this ISmartSoftwareMongoDbContext dbContext)
    {
        var ssMongoDbContext = dbContext as SmartSoftwareMongoDbContext;

        if (ssMongoDbContext == null)
        {
            throw new SmartSoftwareException($"The type {dbContext.GetType().AssemblyQualifiedName} should be convertable to {typeof(SmartSoftwareMongoDbContext).AssemblyQualifiedName}!");
        }

        return ssMongoDbContext;
    }
}
