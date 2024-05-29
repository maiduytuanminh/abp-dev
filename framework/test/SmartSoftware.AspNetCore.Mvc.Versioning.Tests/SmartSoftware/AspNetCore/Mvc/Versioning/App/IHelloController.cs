using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Mvc.Versioning.App;

public interface IHelloController : IRemoteService
{
    Task<string> GetAsync();

    Task<string> PostAsyncV1();

    Task<string> PostAsyncV2();
}
