using System.Threading.Tasks;

namespace SmartSoftware.Ldap;

public interface ILdapManager
{
    Task<bool> AuthenticateAsync(string username, string password);
}
