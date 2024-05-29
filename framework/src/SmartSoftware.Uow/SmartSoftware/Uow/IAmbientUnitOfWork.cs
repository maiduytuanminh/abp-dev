namespace SmartSoftware.Uow;

public interface IAmbientUnitOfWork : IUnitOfWorkAccessor
{
    IUnitOfWork? GetCurrentByChecking();
}
