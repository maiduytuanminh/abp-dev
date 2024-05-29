using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Guids;
using SmartSoftware.SecurityLog;
using SmartSoftware.Uow;

namespace SmartSoftware.Identity;

[Dependency(ReplaceServices = true)]
public class IdentitySecurityLogStore : ISecurityLogStore, ITransientDependency
{
    public ILogger<IdentitySecurityLogStore> Logger { get; set; }

    protected SmartSoftwareSecurityLogOptions SecurityLogOptions { get; }
    protected IIdentitySecurityLogRepository IdentitySecurityLogRepository { get; }
    protected IGuidGenerator GuidGenerator { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public IdentitySecurityLogStore(
        ILogger<IdentitySecurityLogStore> logger,
        IOptions<SmartSoftwareSecurityLogOptions> securityLogOptions,
        IIdentitySecurityLogRepository identitySecurityLogRepository,
        IGuidGenerator guidGenerator,
        IUnitOfWorkManager unitOfWorkManager)
    {
        Logger = logger;
        SecurityLogOptions = securityLogOptions.Value;
        IdentitySecurityLogRepository = identitySecurityLogRepository;
        GuidGenerator = guidGenerator;
        UnitOfWorkManager = unitOfWorkManager;
    }

    public async Task SaveAsync(SecurityLogInfo securityLogInfo)
    {
        if (!SecurityLogOptions.IsEnabled)
        {
            return;
        }

        using (var uow = UnitOfWorkManager.Begin(requiresNew: true))
        {
            await IdentitySecurityLogRepository.InsertAsync(new IdentitySecurityLog(GuidGenerator, securityLogInfo));
            await uow.CompleteAsync();
        }
    }
}
