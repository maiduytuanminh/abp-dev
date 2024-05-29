using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.Identity;

namespace SmartSoftware.Account;

public interface IProfileAppService : IApplicationService
{
    Task<ProfileDto> GetAsync();

    Task<ProfileDto> UpdateAsync(UpdateProfileDto input);

    Task ChangePasswordAsync(ChangePasswordInput input);
}
