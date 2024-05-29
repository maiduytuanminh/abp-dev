using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.AspNetCore.Mvc.Versioning.App.v2;

public interface ITodoAppService : IApplicationService
{
    Task<string> GetAsync(int id);
}
