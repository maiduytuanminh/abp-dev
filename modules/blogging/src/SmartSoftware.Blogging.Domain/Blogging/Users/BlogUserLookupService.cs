using SmartSoftware.Uow;
using SmartSoftware.Users;

namespace SmartSoftware.Blogging.Users
{
    public class BlogUserLookupService : UserLookupService<BlogUser, IBlogUserRepository>, IBlogUserLookupService
    {
        public BlogUserLookupService(
            IBlogUserRepository userRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                userRepository,
                unitOfWorkManager)
        {
            
        }

        protected override BlogUser CreateUser(IUserData externalUser)
        {
            return new BlogUser(externalUser);
        }
    }
}