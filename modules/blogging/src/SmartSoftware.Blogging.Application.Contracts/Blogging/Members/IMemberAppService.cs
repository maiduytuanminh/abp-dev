using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.Blogging.Posts;

namespace SmartSoftware.Blogging.Members;

public interface IMemberAppService : IApplicationService
{
    Task<BlogUserDto> FindAsync(string username);
    
    Task UpdateUserProfileAsync(CustomIdentityBlogUserUpdateDto input);
}