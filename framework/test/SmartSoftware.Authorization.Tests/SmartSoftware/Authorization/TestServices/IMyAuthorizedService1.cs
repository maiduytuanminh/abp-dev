using System.Threading.Tasks;

namespace SmartSoftware.Authorization.TestServices;

public interface IMyAuthorizedService1
{
    Task<int> Anonymous();

    Task<int> AnonymousAsync();

    Task<int> ProtectedByClass();

    Task<int> ProtectedByClassAsync();
}
