﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.MongoDB;

namespace SmartSoftware.FeatureManagement.MongoDB;

public class MongoFeatureValueRepository :
    MongoDbRepository<IFeatureManagementMongoDbContext, FeatureValue, Guid>,
    IFeatureValueRepository
{
    public MongoFeatureValueRepository(IMongoDbContextProvider<IFeatureManagementMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public virtual async Task<FeatureValue> FindAsync(
        string name,
        string providerName,
        string providerKey,
        CancellationToken cancellationToken = default)
    {
        return await (await GetMongoQueryableAsync(cancellationToken))
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync(s => s.Name == name && s.ProviderName == providerName && s.ProviderKey == providerKey, GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<FeatureValue>> FindAllAsync(
        string name,
        string providerName,
        string providerKey,
        CancellationToken cancellationToken = default)
    {
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(s => s.Name == name && s.ProviderName == providerName && s.ProviderKey == providerKey).ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<FeatureValue>> GetListAsync(
        string providerName,
        string providerKey,
        CancellationToken cancellationToken = default)
    {
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(s => s.ProviderName == providerName && s.ProviderKey == providerKey)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task DeleteAsync(
        string providerName,
        string providerKey,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();

        await dbContext.FeatureValues
            .DeleteManyAsync(x => x.ProviderName == providerName && x.ProviderKey == providerKey, GetCancellationToken(cancellationToken));

    }
}
