﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.BlobStoring.Database.EntityFrameworkCore;

public class EfCoreDatabaseBlobRepository : EfCoreRepository<IBlobStoringDbContext, DatabaseBlob, Guid>,
    IDatabaseBlobRepository
{
    public EfCoreDatabaseBlobRepository(IDbContextProvider<IBlobStoringDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public virtual async Task<DatabaseBlob> FindAsync(
        Guid containerId,
        string name,
        CancellationToken cancellationToken = default)
    {
        return (await GetDbSetAsync())
            .FirstOrDefault(
            x => x.ContainerId == containerId && x.Name == name
        );
    }

    public virtual async Task<bool> ExistsAsync(
        Guid containerId,
        string name,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .AnyAsync(
                x => x.ContainerId == containerId && x.Name == name,
                GetCancellationToken(cancellationToken)
            );
    }

    public virtual async Task<bool> DeleteAsync(
        Guid containerId,
        string name,
        bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        //TODO: Should extract this logic to out of the repository and remove this method completely

        var blob = await FindAsync(containerId, name, cancellationToken);
        if (blob == null)
        {
            return false;
        }

        await base.DeleteAsync(blob, autoSave, cancellationToken: GetCancellationToken(cancellationToken));
        return true;
    }
}
