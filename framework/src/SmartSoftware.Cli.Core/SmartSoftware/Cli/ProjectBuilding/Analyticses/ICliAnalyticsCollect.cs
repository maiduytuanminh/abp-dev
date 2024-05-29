using System.Threading.Tasks;

namespace SmartSoftware.Cli.ProjectBuilding.Analyticses;

public interface ICliAnalyticsCollect
{
    Task CollectAsync(CliAnalyticsCollectInputDto input);
}
