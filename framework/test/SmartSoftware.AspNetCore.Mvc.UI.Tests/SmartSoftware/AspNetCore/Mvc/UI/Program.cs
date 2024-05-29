using Microsoft.AspNetCore.Builder;
using SmartSoftware.AspNetCore;
using SmartSoftware.AspNetCore.Mvc.UI;
using SmartSoftware.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunSmartSoftwareModuleAsync<SmartSoftwareAspNetCoreMvcUiTestModule>();

public partial class Program
{
}
