using IdentityModel;
using OpenIddict.Demo.Client.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies", options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(365);
    })
    .AddSmartSoftwareOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:44301/";
        options.RequireHttpsMetadata = true;
        options.ResponseType = OidcConstants.ResponseTypes.Code;

        options.ClientId = "SmartSoftwareApp";
        options.ClientSecret = "1q2w3e*";

        options.UsePkce = true;
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.Scope.Add("email");
        options.Scope.Add("roles");
        options.Scope.Add("phone");
        options.Scope.Add("SmartSoftwareAPI");
    });

await builder.AddApplicationAsync<OpenIddictMvcModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseConfiguredEndpoints();
app.Run();