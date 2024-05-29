using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public interface IApplicationConfigurationContributor
{
    Task ContributeAsync(ApplicationConfigurationContributorContext context);
}
