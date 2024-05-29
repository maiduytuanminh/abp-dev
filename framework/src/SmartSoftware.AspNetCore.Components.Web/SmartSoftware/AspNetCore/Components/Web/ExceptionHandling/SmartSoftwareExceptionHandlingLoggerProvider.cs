using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartSoftware.AspNetCore.Components.Web.ExceptionHandling;

public class SmartSoftwareExceptionHandlingLoggerProvider : ILoggerProvider
{
    private SmartSoftwareExceptionHandlingLogger? _logger;
    private static readonly object SyncObj = new object();
    private readonly IServiceCollection _serviceCollection;

    public SmartSoftwareExceptionHandlingLoggerProvider(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (_logger == null)
        {
            lock (SyncObj)
            {
                if (_logger == null)
                {
                    _logger = new SmartSoftwareExceptionHandlingLogger(_serviceCollection);
                }
            }
        }

        return _logger;
    }

    public void Dispose()
    {

    }
}
