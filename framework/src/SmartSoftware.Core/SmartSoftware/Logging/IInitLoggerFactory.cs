namespace SmartSoftware.Logging;

public interface IInitLoggerFactory
{
    IInitLogger<T> Create<T>();
}
