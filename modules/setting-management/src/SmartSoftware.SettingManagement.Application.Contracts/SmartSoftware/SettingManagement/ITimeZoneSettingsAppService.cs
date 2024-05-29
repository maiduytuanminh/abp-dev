using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.SettingManagement;

public interface ITimeZoneSettingsAppService : IApplicationService
{
    Task<string> GetAsync();

    Task<List<NameValue>> GetTimezonesAsync();

    Task UpdateAsync(string timezone);
}
