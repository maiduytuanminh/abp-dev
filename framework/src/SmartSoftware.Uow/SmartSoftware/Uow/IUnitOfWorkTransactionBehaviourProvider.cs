namespace SmartSoftware.Uow;

public interface IUnitOfWorkTransactionBehaviourProvider
{
    bool? IsTransactional { get; }
}
