using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.Account;

public interface IDynamicClaimsAppService : IApplicationService
{
    Task RefreshAsync();
}
