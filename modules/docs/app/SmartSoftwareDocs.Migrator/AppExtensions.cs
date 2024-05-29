using Microsoft.Extensions.DependencyInjection;
using SmartSoftware;

namespace SmartSoftwareDocs.Migrator
{
    public static class AppExtensions
    {
        public static T Resolve<T>(this ISmartSoftwareApplicationWithInternalServiceProvider app)
        {
            return (T)app.ServiceProvider.GetRequiredService<T>();
        }
    }
}