using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Uow;

public class NullUnitOfWorkTransactionBehaviourProvider : IUnitOfWorkTransactionBehaviourProvider, ISingletonDependency
{
    public bool? IsTransactional => null;
}
