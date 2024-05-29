using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSoftware.Uow;

public interface ITransactionApi : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
