using Microsoft.Extensions.Logging;

namespace SmartSoftware.Logging;

public interface IExceptionWithSelfLogging
{
    void Log(ILogger logger);
}
