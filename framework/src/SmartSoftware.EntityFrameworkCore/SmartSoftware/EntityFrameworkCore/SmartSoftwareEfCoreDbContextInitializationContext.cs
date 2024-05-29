using SmartSoftware.Uow;

namespace SmartSoftware.EntityFrameworkCore;

public class SmartSoftwareEfCoreDbContextInitializationContext
{
    public IUnitOfWork UnitOfWork { get; }

    public SmartSoftwareEfCoreDbContextInitializationContext(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}
