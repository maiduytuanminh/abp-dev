using System;
using System.Threading.Tasks;
using SmartSoftware.Http.Modeling;

namespace SmartSoftware.Http.Client.DynamicProxying;

public interface IApiDescriptionCache
{
    Task<ApplicationApiDescriptionModel> GetAsync(
        string baseUrl,
        Func<Task<ApplicationApiDescriptionModel>> factory
    );
}
