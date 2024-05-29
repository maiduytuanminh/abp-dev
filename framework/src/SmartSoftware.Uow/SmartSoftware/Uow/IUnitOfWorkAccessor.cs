using JetBrains.Annotations;

namespace SmartSoftware.Uow;

public interface IUnitOfWorkAccessor
{
    IUnitOfWork? UnitOfWork { get; }

    void SetUnitOfWork(IUnitOfWork? unitOfWork);
}
