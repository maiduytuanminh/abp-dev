using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware.Domain.Repositories;

namespace SmartSoftware.BlobStoring.Database;

public interface IDatabaseBlobContainerRepository : IBasicRepository<DatabaseBlobContainer, Guid>
{
    Task<DatabaseBlobContainer> FindAsync([NotNull] string name, CancellationToken cancellationToken = default);
}
