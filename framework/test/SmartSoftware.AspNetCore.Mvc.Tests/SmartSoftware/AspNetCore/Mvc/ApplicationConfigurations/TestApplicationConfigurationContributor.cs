using System.Threading.Tasks;
using SmartSoftware.Data;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public class TestApplicationConfigurationContributor : IApplicationConfigurationContributor
{
    public Task ContributeAsync(ApplicationConfigurationContributorContext context)
    {
        context.ApplicationConfiguration.SetProperty("TestKey", "TestValue");
        return Task.CompletedTask;
    }
}
