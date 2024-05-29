using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Docs.Documents
{
    public sealed class NullNavigationTreePostProcessor : INavigationTreePostProcessor
    {
        public static NullNavigationTreePostProcessor Instance { get; } = new NullNavigationTreePostProcessor();

        private NullNavigationTreePostProcessor()
        {
            
        }
        
        public Task ProcessAsync(NavigationTreePostProcessorContext context)
        {
            return Task.CompletedTask;
        }
    }
}