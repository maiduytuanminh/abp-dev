using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Cli.ServiceProxying;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands;

public class RemoveProxyCommand : ProxyCommandBase<RemoveProxyCommand>
{
    public const string Name = "remove-proxy";

    protected override string CommandName => Name;

    public RemoveProxyCommand(
        IOptions<SmartSoftwareCliServiceProxyOptions> serviceProxyOptions,
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceProxyOptions, serviceScopeFactory)
    {
    }

    public override string GetUsageInfo()
    {
        var sb = new StringBuilder(base.GetUsageInfo());

        sb.AppendLine("");
        sb.AppendLine("Examples:");
        sb.AppendLine("");
        sb.AppendLine("  ss remove-proxy -t ng");
        sb.AppendLine("  ss remove-proxy -t js -m identity -o Pages/Identity/client-proxies.js");
        sb.AppendLine("  ss remove-proxy -t csharp --folder MyProxies/InnerFolder");

        return sb.ToString();
    }

    public override string GetShortDescription()
    {
        return "Remove client service proxies and DTOs to consume HTTP APIs.";
    }
}
