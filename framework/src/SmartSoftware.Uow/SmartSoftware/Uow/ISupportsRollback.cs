using System.Threading;
using System.Threading.Tasks;

namespace SmartSoftware.Uow;

public interface ISupportsRollback
{
    Task RollbackAsync(CancellationToken cancellationToken = default);
}
