using SmartSoftware.Uow;
using SmartSoftware.Users;

namespace SmartSoftware.CmsKit.Users;

public class CmsUserLookupService : UserLookupService<CmsUser, ICmsUserRepository>, ICmsUserLookupService
{
    public CmsUserLookupService(
        ICmsUserRepository userRepository,
        IUnitOfWorkManager unitOfWorkManager)
        : base(
            userRepository,
            unitOfWorkManager)
    {

    }

    protected override CmsUser CreateUser(IUserData externalUser)
    {
        return new CmsUser(externalUser);
    }
}
