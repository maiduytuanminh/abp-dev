using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using SmartSoftware.AspNetCore;
using SmartSoftware.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Staging
});
await builder.RunSmartSoftwareModuleAsync<SmartSoftwareAspNetCoreTestModule>();

public partial class Program
{
}
