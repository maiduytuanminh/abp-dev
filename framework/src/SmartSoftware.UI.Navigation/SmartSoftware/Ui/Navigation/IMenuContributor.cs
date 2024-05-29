using System.Threading.Tasks;

namespace SmartSoftware.UI.Navigation;

public interface IMenuContributor
{
    Task ConfigureMenuAsync(MenuConfigurationContext context);
}
