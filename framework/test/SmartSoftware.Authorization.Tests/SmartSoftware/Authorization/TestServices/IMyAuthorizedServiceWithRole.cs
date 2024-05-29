using System.Threading.Tasks;

namespace SmartSoftware.Authorization.TestServices;

public interface IMyAuthorizedServiceWithRole
{
    Task<int> ProtectedByRole();

    Task<int> ProtectedByScheme();

    Task<int> ProtectedByAnotherRole();
}
