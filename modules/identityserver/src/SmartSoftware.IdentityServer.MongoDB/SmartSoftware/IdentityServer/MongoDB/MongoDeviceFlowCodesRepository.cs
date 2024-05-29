using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.MongoDB;

namespace SmartSoftware.IdentityServer.MongoDB;

public class MongoDeviceFlowCodesRepository :
    MongoDbRepository<ISmartSoftwareIdentityServerMongoDbContext, DeviceFlowCodes, Guid>, IDeviceFlowCodesRepository
{
    public MongoDeviceFlowCodesRepository(
        IMongoDbContextProvider<ISmartSoftwareIdentityServerMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }

    public virtual async Task<DeviceFlowCodes> FindByUserCodeAsync(
        string userCode,
        CancellationToken cancellationToken = default)
    {
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(d => d.UserCode == userCode)
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<DeviceFlowCodes> FindByDeviceCodeAsync(string deviceCode, CancellationToken cancellationToken = default)
    {
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(d => d.DeviceCode == deviceCode)
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<DeviceFlowCodes>> GetListByExpirationAsync(
        DateTime maxExpirationDate,
        int maxResultCount,
        CancellationToken cancellationToken = default)
    {
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(x => x.Expiration != null && x.Expiration < maxExpirationDate)
            .OrderBy(x => x.ClientId)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task DeleteExpirationAsync(DateTime maxExpirationDate, CancellationToken cancellationToken = default)
    {
        await DeleteDirectAsync(x => x.Expiration != null && x.Expiration < maxExpirationDate, cancellationToken: cancellationToken);
    }
}
