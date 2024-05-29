using Microsoft.AspNetCore.SignalR;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;
using SmartSoftware.Users;

namespace SmartSoftware.AspNetCore.SignalR;

public class SmartSoftwareSignalRUserIdProvider : IUserIdProvider, ITransientDependency
{
    private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

    private readonly ICurrentUser _currentUser;

    public SmartSoftwareSignalRUserIdProvider(ICurrentPrincipalAccessor currentPrincipalAccessor, ICurrentUser currentUser)
    {
        _currentPrincipalAccessor = currentPrincipalAccessor;
        _currentUser = currentUser;
    }

    public virtual string? GetUserId(HubConnectionContext connection)
    {
        using (_currentPrincipalAccessor.Change(connection.User))
        {
            return _currentUser.Id?.ToString();
        }
    }
}
