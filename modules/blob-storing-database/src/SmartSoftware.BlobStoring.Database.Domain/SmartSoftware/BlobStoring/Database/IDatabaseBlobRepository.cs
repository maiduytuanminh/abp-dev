﻿using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware.Domain.Repositories;

namespace SmartSoftware.BlobStoring.Database;

public interface IDatabaseBlobRepository : IBasicRepository<DatabaseBlob, Guid>
{
    Task<DatabaseBlob> FindAsync(Guid containerId, [NotNull] string name, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid containerId, [NotNull] string name, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid containerId, [NotNull] string name, bool autoSave = false, CancellationToken cancellationToken = default);
}
