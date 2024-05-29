using System.Linq;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using SmartSoftware.AspNetCore.App;

namespace SmartSoftware.AspNetCore.Serilog;

public class SmartSoftwareSerilogTestBase : SmartSoftwareAspNetCoreTestBase<Program>
{
    protected readonly CollectingSink CollectingSink = new CollectingSink();

    protected override IHost CreateHost(IHostBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Sink(CollectingSink)
            .CreateLogger();

        builder.UseSerilog();
        return base.CreateHost(builder);;
    }

    protected LogEvent GetLogEvent(string text)
    {
        return CollectingSink.Events.FirstOrDefault(m => m.MessageTemplate.Text == text);
    }
}
