using System.Threading.Tasks;

namespace SmartSoftware.Authorization.TestServices;

public interface IMySimpleAuthorizedService
{
    Task<int> ProtectedByClassAsync();

    Task<int> AnonymousAsync();
}
