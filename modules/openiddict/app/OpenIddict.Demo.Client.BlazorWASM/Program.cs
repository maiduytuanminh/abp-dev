using IdentityModel;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenIddict.Demo.Client.BlazorWASM;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://localhost:44301/";
    options.ProviderOptions.ClientId = "SmartSoftwareBlazorWASMApp";
    options.ProviderOptions.ResponseType = "code";

    options.UserOptions.NameClaim = JwtClaimTypes.Name;
    options.UserOptions.RoleClaim = JwtClaimTypes.Role;

    options.ProviderOptions.DefaultScopes.Add("roles");
    options.ProviderOptions.DefaultScopes.Add("email");
    options.ProviderOptions.DefaultScopes.Add("phone");
    options.ProviderOptions.DefaultScopes.Add("SmartSoftwareAPI");
});

await builder.Build().RunAsync();
