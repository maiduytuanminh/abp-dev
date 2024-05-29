using System.Threading.Tasks;

namespace SmartSoftware.PermissionManagement;

public interface IStaticPermissionSaver
{
    Task SaveAsync();
}