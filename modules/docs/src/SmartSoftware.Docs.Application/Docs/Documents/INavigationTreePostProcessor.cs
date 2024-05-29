using System.Threading.Tasks;

namespace SmartSoftware.Docs.Documents
{
    public interface INavigationTreePostProcessor
    {
        Task ProcessAsync(NavigationTreePostProcessorContext context);
    }
}