using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Options;
using SmartSoftware.Users;

namespace SmartSoftware.AspNetCore.Components.Web.Security;

public class SmartSoftwareAuthenticationState : ComponentBase
{
    private const string StateKey = "authentication-state-id";
    
    [Inject]
    protected ILocalStorageService LocalStorage { get; set; } = default!;
    
    [Inject]
    protected ICurrentUser CurrentUser { get; set; } = default!;
    
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;
    
    [Inject]
    protected IOptions<SmartSoftwareAuthenticationOptions> AuthenticationOptions { get; set; } = default!;

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            NavigationManager.RegisterLocationChangingHandler(OnLocationChangingAsync);
            if (CurrentUser.IsAuthenticated)
            {
                await SetAuthenticationStateAsync();
            }
            else
            {
                await ClearAuthenticationStateAsync();
            }
        }
    }
    
    protected virtual async ValueTask OnLocationChangingAsync(LocationChangingContext context)
    {
        if (context.TargetLocation.Contains(AuthenticationOptions.Value.LogoutUrl))
        {
            await ClearAuthenticationStateAsync();
        }
    }
    
    protected virtual async Task SetAuthenticationStateAsync()
    {
        await LocalStorage.SetItemAsync(StateKey, CurrentUser.GetId().ToString());
    }
    
    protected virtual async Task ClearAuthenticationStateAsync()
    {
        await LocalStorage.RemoveItemAsync(StateKey);
    }
}