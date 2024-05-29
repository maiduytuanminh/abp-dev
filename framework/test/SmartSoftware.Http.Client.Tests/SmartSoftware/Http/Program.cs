using Microsoft.AspNetCore.Builder;
using SmartSoftware.AspNetCore;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Http;

var builder = WebApplication.CreateBuilder();
await builder.RunSmartSoftwareModuleAsync<SmartSoftwareHttpClientTestModule>();

public partial class Program
{
}
