using Microsoft.AspNetCore.Builder;
using SmartSoftware.AspNetCore;
using SmartSoftware.AspNetCore.Mvc.Versioning;
using SmartSoftware.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunSmartSoftwareModuleAsync<SmartSoftwareAspNetCoreMvcVersioningTestModule>();

public partial class Program
{
}
