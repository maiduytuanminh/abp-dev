using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSoftware;

namespace SmartSoftware.CmsKit;

public class ConsoleTestAppHostedService : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var application = SmartSoftwareApplicationFactory.Create<CmsKitConsoleApiClientModule>())
        {
            application.Initialize();

            var demo = application.ServiceProvider.GetRequiredService<ClientDemoService>();
            await demo.RunAsync();

            application.Shutdown();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
