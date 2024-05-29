using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SmartSoftware.AspNetCore.TestBase;

public static class SmartSoftwareWebHostBuilderExtensions
{
    public static IWebHostBuilder UseSmartSoftwareTestServer(this IWebHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddScoped<IHostLifetime, SmartSoftwareNoopHostLifetime>();
            services.AddScoped<IServer, TestServer>();
        });
    }
}
