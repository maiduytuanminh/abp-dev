using Microsoft.AspNetCore.Builder;
using SmartSoftware.AspNetCore;
using SmartSoftware.AspNetCore.App;
using SmartSoftware.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunSmartSoftwareModuleAsync<AppModule>();

public partial class Program
{
}
