using System.Threading.Tasks;

namespace SmartSoftware.Application.Services;

public interface IDeleteAppService<in TKey> : IApplicationService
{
    Task DeleteAsync(TKey id);
}
