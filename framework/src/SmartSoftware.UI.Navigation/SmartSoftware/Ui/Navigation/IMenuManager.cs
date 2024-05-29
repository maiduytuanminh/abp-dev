using System.Threading.Tasks;

namespace SmartSoftware.UI.Navigation;

public interface IMenuManager
{
    Task<ApplicationMenu> GetAsync(string name);

    Task<ApplicationMenu> GetMainMenuAsync();
}
