using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.AspNetCore.Mvc.Versioning.App.v1;

public interface ITodoAppService : IApplicationService
{
    Task<string> GetAsync(int id);
}
