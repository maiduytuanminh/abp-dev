using Microsoft.AspNetCore.Builder;
using MyCompanyName.MyProjectName;
using SmartSoftware.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunSmartSoftwareModuleAsync<MyProjectNameWebTestModule>();

public partial class Program
{
}
