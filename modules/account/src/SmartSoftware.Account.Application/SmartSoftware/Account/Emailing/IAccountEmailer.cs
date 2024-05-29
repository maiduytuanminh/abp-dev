using System.Threading.Tasks;
using SmartSoftware.Identity;

namespace SmartSoftware.Account.Emailing;

public interface IAccountEmailer
{
    Task SendPasswordResetLinkAsync(
        IdentityUser user,
        string resetToken,
        string appName,
        string returnUrl = null,
        string returnUrlHash = null
    );
}
