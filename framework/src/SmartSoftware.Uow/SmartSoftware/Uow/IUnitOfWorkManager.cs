using JetBrains.Annotations;

namespace SmartSoftware.Uow;

public interface IUnitOfWorkManager
{
    IUnitOfWork? Current { get; }

    [NotNull]
    IUnitOfWork Begin([NotNull] SmartSoftwareUnitOfWorkOptions options, bool requiresNew = false);

    [NotNull]
    IUnitOfWork Reserve([NotNull] string reservationName, bool requiresNew = false);

    void BeginReserved([NotNull] string reservationName, [NotNull] SmartSoftwareUnitOfWorkOptions options);

    bool TryBeginReserved([NotNull] string reservationName, [NotNull] SmartSoftwareUnitOfWorkOptions options);
}
