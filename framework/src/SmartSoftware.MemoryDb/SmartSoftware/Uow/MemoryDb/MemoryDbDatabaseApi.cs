using SmartSoftware.Domain.Repositories.MemoryDb;

namespace SmartSoftware.Uow.MemoryDb;

public class MemoryDbDatabaseApi : IDatabaseApi
{
    public IMemoryDatabase Database { get; }

    public MemoryDbDatabaseApi(IMemoryDatabase database)
    {
        Database = database;
    }
}
