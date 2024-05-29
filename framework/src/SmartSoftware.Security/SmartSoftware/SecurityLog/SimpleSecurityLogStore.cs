using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.SecurityLog;

public class SimpleSecurityLogStore : ISecurityLogStore, ITransientDependency
{
    public ILogger<SimpleSecurityLogStore> Logger { get; set; }
    protected SmartSoftwareSecurityLogOptions SecurityLogOptions { get; }

    public SimpleSecurityLogStore(ILogger<SimpleSecurityLogStore> logger, IOptions<SmartSoftwareSecurityLogOptions> securityLogOptions)
    {
        Logger = logger;
        SecurityLogOptions = securityLogOptions.Value;
    }

    public Task SaveAsync(SecurityLogInfo securityLogInfo)
    {
        if (!SecurityLogOptions.IsEnabled)
        {
            return Task.CompletedTask;
        }

        Logger.LogInformation(securityLogInfo.ToString());
        return Task.CompletedTask;
    }
}
