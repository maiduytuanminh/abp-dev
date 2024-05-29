using System.Threading.Tasks;

namespace SmartSoftware.SettingManagement;

public interface IStaticSettingSaver
{
    Task SaveAsync();
}
