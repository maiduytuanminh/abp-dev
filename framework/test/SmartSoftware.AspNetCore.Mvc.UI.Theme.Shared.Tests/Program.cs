using Microsoft.AspNetCore.Builder;
using SmartSoftware.AspNetCore;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Tests.SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunSmartSoftwareModuleAsync<SmartSoftwareAspNetCoreMvcUiThemeSharedTestModule>();

public partial class Program
{
}
