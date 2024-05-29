using Hangfire;

namespace SmartSoftware.Hangfire;

public class SmartSoftwareHangfireBackgroundJobServer
{
    public BackgroundJobServer? HangfireJobServer { get; }

    public SmartSoftwareHangfireBackgroundJobServer(BackgroundJobServer? hangfireJobServer)
    {
        HangfireJobServer = hangfireJobServer;
    }
}
