using Microsoft.AspNetCore.Builder;
using SmartSoftware.AspNetCore.SignalR;
using SmartSoftware.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunSmartSoftwareModuleAsync<SmartSoftwareAspNetCoreSignalRTestModule>();

public partial class Program
{
}
