# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

function Write-Info   
{
	param(
        [Parameter(Mandatory = $true)]
        [string]
        $text
    )

	Write-Host $text -ForegroundColor Black -BackgroundColor Green

	try 
	{
	   $host.UI.RawUI.WindowTitle = $text
	}		
	catch 
	{
		#Changing window title is not suppoerted!
	}
}

function Write-Error   
{
	param(
        [Parameter(Mandatory = $true)]
        [string]
        $text
    )

	Write-Host $text -ForegroundColor Red -BackgroundColor Black 
}

function Seperator   
{
	Write-Host ("_" * 100)  -ForegroundColor gray 
}

function Get-Current-Version { 
	$commonPropsFilePath = resolve-path "../common.props"
	$commonPropsXmlCurrent = [xml](Get-Content $commonPropsFilePath ) 
	$currentVersion = $commonPropsXmlCurrent.Project.PropertyGroup.Version.Trim()
	return $currentVersion
}

function Get-Current-Branch {
	return git branch --show-current
}	   

function Read-File {
	param(
        [Parameter(Mandatory = $true)]
        [string]
        $filePath
    )
		
	$pathExists = Test-Path -Path $filePath -PathType Leaf
	if ($pathExists)
	{
		return Get-Content $filePath		
	}
	else{
		Write-Error  "$filePath path does not exist!"
	}
}

# List of solutions
$solutions = (
    "framework",
    "modules/account",
    "modules/audit-logging",
    "modules/background-jobs",
    "modules/basic-theme",
    "modules/blogging",
    "modules/client-simulation",
    "modules/docs",
    "modules/feature-management",
    "modules/identity",
    "modules/identityserver",
    "modules/openiddict",
    "modules/permission-management",
    "modules/setting-management",
    "modules/tenant-management",
    "modules/users",
    "modules/virtual-file-explorer",
    "modules/blob-storing-database",
    "modules/cms-kit"
)

# List of projects
$projects = (

    # framework
    "framework/src/SmartSoftware.ApiVersioning.Abstractions",
    "framework/src/SmartSoftware.AspNetCore.Authentication.JwtBearer",
    "framework/src/SmartSoftware.AspNetCore.Authentication.OAuth",
    "framework/src/SmartSoftware.AspNetCore.Authentication.OpenIdConnect",
    "framework/src/SmartSoftware.AspNetCore.Abstractions",
    "framework/src/SmartSoftware.AspNetCore",
    "framework/src/SmartSoftware.AspNetCore.Mvc.Dapr",
    "framework/src/SmartSoftware.AspNetCore.Mvc.Dapr.EventBus",
    "framework/src/SmartSoftware.AspNetCore.Components",
    "framework/src/SmartSoftware.AspNetCore.Components.Server",
    "framework/src/SmartSoftware.AspNetCore.Components.Web",
    "framework/src/SmartSoftware.AspNetCore.Components.MauiBlazor",
    "framework/src/SmartSoftware.AspNetCore.Components.Web.Theming",
    "framework/src/SmartSoftware.AspNetCore.Components.WebAssembly",
    "framework/src/SmartSoftware.AspNetCore.Components.WebAssembly.Theming",
    "framework/src/SmartSoftware.AspNetCore.Components.Server",
    "framework/src/SmartSoftware.AspNetCore.Components.Server.Theming",
    "framework/src/SmartSoftware.AspNetCore.Components.MauiBlazor.Theming",    
    "framework/src/SmartSoftware.AspNetCore.MultiTenancy",
    "framework/src/SmartSoftware.AspNetCore.Mvc.Client",
    "framework/src/SmartSoftware.AspNetCore.Mvc.Client.Common",
    "framework/src/SmartSoftware.AspNetCore.Mvc.Contracts",
    "framework/src/SmartSoftware.AspNetCore.Mvc",
    "framework/src/SmartSoftware.AspNetCore.Mvc.NewtonsoftJson",
    "framework/src/SmartSoftware.AspNetCore.Mvc.UI.Bootstrap",
    "framework/src/SmartSoftware.AspNetCore.Mvc.UI.Bundling.Abstractions",
    "framework/src/SmartSoftware.AspNetCore.Mvc.UI.Bundling",
    "framework/src/SmartSoftware.AspNetCore.Mvc.UI",
    "framework/src/SmartSoftware.AspNetCore.Mvc.UI.MultiTenancy",
    "framework/src/SmartSoftware.AspNetCore.Mvc.UI.Packages",
    "framework/src/SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared",
    "framework/src/SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Demo",
    "framework/src/SmartSoftware.AspNetCore.Mvc.UI.Widgets",
    "framework/src/SmartSoftware.AspNetCore.Serilog",
    "framework/src/SmartSoftware.AspNetCore.SignalR",
    "framework/src/SmartSoftware.AspNetCore.TestBase",
    "framework/src/SmartSoftware.Auditing.Contracts",
    "framework/src/SmartSoftware.Auditing",
    "framework/src/SmartSoftware.Authorization",
    "framework/src/SmartSoftware.Authorization.Abstractions",
    "framework/src/SmartSoftware.Autofac",
    "framework/src/SmartSoftware.Autofac.WebAssembly",
    "framework/src/SmartSoftware.AutoMapper",
    "framework/src/SmartSoftware.AzureServiceBus",
    "framework/src/SmartSoftware.BackgroundJobs.Abstractions",
    "framework/src/SmartSoftware.BackgroundJobs",
    "framework/src/SmartSoftware.BackgroundJobs.HangFire",
    "framework/src/SmartSoftware.BackgroundJobs.RabbitMQ",
    "framework/src/SmartSoftware.BackgroundJobs.Quartz",
    "framework/src/SmartSoftware.BackgroundWorkers",
    "framework/src/SmartSoftware.BackgroundWorkers.Quartz",
    "framework/src/SmartSoftware.BackgroundWorkers.Hangfire",
    "framework/src/SmartSoftware.BlazoriseUI",
    "framework/src/SmartSoftware.BlobStoring",
    "framework/src/SmartSoftware.BlobStoring.FileSystem",
    "framework/src/SmartSoftware.BlobStoring.Aliyun",
    "framework/src/SmartSoftware.BlobStoring.Azure",
    "framework/src/SmartSoftware.BlobStoring.Minio",
    "framework/src/SmartSoftware.BlobStoring.Aws",
    "framework/src/SmartSoftware.Caching",
    "framework/src/SmartSoftware.Caching.StackExchangeRedis",
    "framework/src/SmartSoftware.Castle.Core",
    "framework/src/SmartSoftware.Cli.Core",
    "framework/src/SmartSoftware.Cli",
    "framework/src/SmartSoftware.Core",
    "framework/src/SmartSoftware",
    "framework/src/SmartSoftware.Dapper",
    "framework/src/SmartSoftware.Dapr",
    "framework/src/SmartSoftware.Data",
    "framework/src/SmartSoftware.Ddd.Application",
    "framework/src/SmartSoftware.Ddd.Application.Contracts",
    "framework/src/SmartSoftware.Ddd.Domain",
    "framework/src/SmartSoftware.Ddd.Domain.Shared",
    "framework/src/SmartSoftware.DistributedLocking.Abstractions",
    "framework/src/SmartSoftware.DistributedLocking",
    "framework/src/SmartSoftware.DistributedLocking.Dapr",
    "framework/src/SmartSoftware.Emailing",
    "framework/src/SmartSoftware.EntityFrameworkCore",
    "framework/src/SmartSoftware.EntityFrameworkCore.MySQL",
    "framework/src/SmartSoftware.EntityFrameworkCore.Oracle",
    "framework/src/SmartSoftware.EntityFrameworkCore.Oracle.Devart",
    "framework/src/SmartSoftware.EntityFrameworkCore.PostgreSql",
    "framework/src/SmartSoftware.EntityFrameworkCore.Sqlite",
    "framework/src/SmartSoftware.EntityFrameworkCore.SqlServer",
    "framework/src/SmartSoftware.EventBus.Abstractions",
    "framework/src/SmartSoftware.EventBus",
    "framework/src/SmartSoftware.EventBus.RabbitMQ",
    "framework/src/SmartSoftware.EventBus.Kafka",
    "framework/src/SmartSoftware.EventBus.Rebus",
    "framework/src/SmartSoftware.EventBus.Azure",
    "framework/src/SmartSoftware.EventBus.Dapr",
    "framework/src/SmartSoftware.ExceptionHandling",
    "framework/src/SmartSoftware.Features",
    "framework/src/SmartSoftware.FluentValidation",
    "framework/src/SmartSoftware.Gdpr.Abstractions",
    "framework/src/SmartSoftware.GlobalFeatures",
    "framework/src/SmartSoftware.Guids",
    "framework/src/SmartSoftware.HangFire",
    "framework/src/SmartSoftware.Http.Abstractions",
    "framework/src/SmartSoftware.Http.Client",
    "framework/src/SmartSoftware.Http.Client.Dapr",
    "framework/src/SmartSoftware.Http.Client.Web",
    "framework/src/SmartSoftware.Http.Client.IdentityModel",
    "framework/src/SmartSoftware.Http.Client.IdentityModel.Web",
    "framework/src/SmartSoftware.Http.Client.IdentityModel.WebAssembly",
    "framework/src/SmartSoftware.Http.Client.IdentityModel.MauiBlazor",
    "framework/src/SmartSoftware.Http",
    "framework/src/SmartSoftware.IdentityModel",
    "framework/src/SmartSoftware.Imaging.Abstractions",
    "framework/src/SmartSoftware.Imaging.AspNetCore",
    "framework/src/SmartSoftware.Imaging.ImageSharp",
    "framework/src/SmartSoftware.Imaging.MagickNet",
    "framework/src/SmartSoftware.Imaging.SkiaSharp",
    "framework/src/SmartSoftware.Json",
    "framework/src/SmartSoftware.Json.Abstractions",
    "framework/src/SmartSoftware.Json.Newtonsoft",
    "framework/src/SmartSoftware.Json.SystemTextJson",
    "framework/src/SmartSoftware.Ldap.Abstractions",
    "framework/src/SmartSoftware.Ldap",
    "framework/src/SmartSoftware.Localization.Abstractions",
    "framework/src/SmartSoftware.MailKit",
    "framework/src/SmartSoftware.Maui.Client",
    "framework/src/SmartSoftware.Localization",
    "framework/src/SmartSoftware.MemoryDb",
    "framework/src/SmartSoftware.MongoDB",
    "framework/src/SmartSoftware.MultiTenancy.Abstractions",
    "framework/src/SmartSoftware.MultiTenancy",
    "framework/src/SmartSoftware.Minify",
    "framework/src/SmartSoftware.ObjectExtending",
    "framework/src/SmartSoftware.ObjectMapping",
    "framework/src/SmartSoftware.Quartz",
    "framework/src/SmartSoftware.RabbitMQ",
    "framework/src/SmartSoftware.RemoteServices",
    "framework/src/SmartSoftware.Security",
    "framework/src/SmartSoftware.Serialization",
    "framework/src/SmartSoftware.Settings",
    "framework/src/SmartSoftware.Sms",
    "framework/src/SmartSoftware.Sms.Aliyun",
    "framework/src/SmartSoftware.Specifications",
    "framework/src/SmartSoftware.TestBase",
    "framework/src/SmartSoftware.TextTemplating",
    "framework/src/SmartSoftware.TextTemplating.Core",
    "framework/src/SmartSoftware.TextTemplating.Razor",
    "framework/src/SmartSoftware.TextTemplating.Scriban",
    "framework/src/SmartSoftware.Threading",
    "framework/src/SmartSoftware.Timing",
    "framework/src/SmartSoftware.UI",
    "framework/src/SmartSoftware.UI.Navigation",
    "framework/src/SmartSoftware.Uow",
    "framework/src/SmartSoftware.Validation.Abstractions",
    "framework/src/SmartSoftware.Validation",
    "framework/src/SmartSoftware.VirtualFileSystem",
    "framework/src/SmartSoftware.Kafka",
    "framework/src/SmartSoftware.Swashbuckle",

    # modules/account
    "modules/account/src/SmartSoftware.Account.Application.Contracts",
    "modules/account/src/SmartSoftware.Account.Application",
    "modules/account/src/SmartSoftware.Account.HttpApi.Client",
    "modules/account/src/SmartSoftware.Account.HttpApi",
    "modules/account/src/SmartSoftware.Account.Web",
    "modules/account/src/SmartSoftware.Account.Web.IdentityServer",
    "modules/account/src/SmartSoftware.Account.Web.OpenIddict",
    "modules/account/src/SmartSoftware.Account.Blazor",
    "modules/account/src/SmartSoftware.Account.Installer",
    "source-code/SmartSoftware.Account.SourceCode",
        
    # modules/audit-logging
    "modules/audit-logging/src/SmartSoftware.AuditLogging.Domain",
    "modules/audit-logging/src/SmartSoftware.AuditLogging.Domain.Shared",
    "modules/audit-logging/src/SmartSoftware.AuditLogging.EntityFrameworkCore",
    "modules/audit-logging/src/SmartSoftware.AuditLogging.MongoDB",
    # "modules/audit-logging/src/SmartSoftware.AuditLogging.Installer",
    # "source-code/SmartSoftware.AuditLogging.SourceCode",

    # modules/background-jobs
    "modules/background-jobs/src/SmartSoftware.BackgroundJobs.Domain",
    "modules/background-jobs/src/SmartSoftware.BackgroundJobs.Domain.Shared",
    "modules/background-jobs/src/SmartSoftware.BackgroundJobs.EntityFrameworkCore",
    "modules/background-jobs/src/SmartSoftware.BackgroundJobs.MongoDB",
    "modules/background-jobs/src/SmartSoftware.BackgroundJobs.Installer",
    "source-code/SmartSoftware.BackgroundJobs.SourceCode",

    # modules/basic-theme
    "modules/basic-theme/src/SmartSoftware.AspNetCore.Components.Server.BasicTheme",
    "modules/basic-theme/src/SmartSoftware.AspNetCore.Components.Web.BasicTheme",
    "modules/basic-theme/src/SmartSoftware.AspNetCore.Components.WebAssembly.BasicTheme",
    "modules/basic-theme/src/SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic",
    "modules/basic-theme/src/SmartSoftware.BasicTheme.Installer",
    "source-code/SmartSoftware.BasicTheme.SourceCode",

    # modules/blogging
    "modules/blogging/src/SmartSoftware.Blogging.Application.Contracts.Shared",
    "modules/blogging/src/SmartSoftware.Blogging.Application.Contracts",
    "modules/blogging/src/SmartSoftware.Blogging.Application",
    "modules/blogging/src/SmartSoftware.Blogging.Domain",
    "modules/blogging/src/SmartSoftware.Blogging.Domain.Shared",
    "modules/blogging/src/SmartSoftware.Blogging.EntityFrameworkCore",
    "modules/blogging/src/SmartSoftware.Blogging.HttpApi.Client",
    "modules/blogging/src/SmartSoftware.Blogging.HttpApi",
    "modules/blogging/src/SmartSoftware.Blogging.MongoDB",
    "modules/blogging/src/SmartSoftware.Blogging.Web",
    "modules/blogging/src/SmartSoftware.Blogging.Admin.Application",
    "modules/blogging/src/SmartSoftware.Blogging.Admin.Application.Contracts",
    "modules/blogging/src/SmartSoftware.Blogging.Admin.HttpApi",
    "modules/blogging/src/SmartSoftware.Blogging.Admin.HttpApi.Client",
    "modules/blogging/src/SmartSoftware.Blogging.Admin.Web",
    "modules/blogging/src/SmartSoftware.Blogging.Installer",
    "source-code/SmartSoftware.Blogging.SourceCode",

    # modules/client-simulation
    "modules/client-simulation/src/SmartSoftware.ClientSimulation",
    "modules/client-simulation/src/SmartSoftware.ClientSimulation.Web",

    # modules/docs
    "modules/docs/src/SmartSoftware.Docs.Admin.Application.Contracts",
    "modules/docs/src/SmartSoftware.Docs.Admin.Application",
    "modules/docs/src/SmartSoftware.Docs.Admin.HttpApi.Client",
    "modules/docs/src/SmartSoftware.Docs.Admin.HttpApi",
    "modules/docs/src/SmartSoftware.Docs.Admin.Web",
    "modules/docs/src/SmartSoftware.Docs.Application.Contracts",
    "modules/docs/src/SmartSoftware.Docs.Application",
    "modules/docs/src/SmartSoftware.Docs.Domain",
    "modules/docs/src/SmartSoftware.Docs.Domain.Shared",
    "modules/docs/src/SmartSoftware.Docs.EntityFrameworkCore",
    "modules/docs/src/SmartSoftware.Docs.HttpApi.Client",
    "modules/docs/src/SmartSoftware.Docs.HttpApi",
    "modules/docs/src/SmartSoftware.Docs.MongoDB",
    "modules/docs/src/SmartSoftware.Docs.Web",
    "modules/docs/src/SmartSoftware.Docs.Installer",
    "source-code/SmartSoftware.Docs.SourceCode",

    # modules/feature-management
    "modules/feature-management/src/SmartSoftware.FeatureManagement.Application.Contracts",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.Application",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.Domain",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.Domain.Shared",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.EntityFrameworkCore",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.HttpApi.Client",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.HttpApi",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.MongoDB",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.Web",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.Blazor",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.Blazor.Server",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.Blazor.WebAssembly",
    "modules/feature-management/src/SmartSoftware.FeatureManagement.Installer",
    "source-code/SmartSoftware.FeatureManagement.SourceCode",

    # modules/identity
    "modules/identity/src/SmartSoftware.Identity.Application.Contracts",
    "modules/identity/src/SmartSoftware.Identity.Application",
    "modules/identity/src/SmartSoftware.Identity.AspNetCore",
    "modules/identity/src/SmartSoftware.Identity.Domain",
    "modules/identity/src/SmartSoftware.Identity.Domain.Shared",
    "modules/identity/src/SmartSoftware.Identity.EntityFrameworkCore",
    "modules/identity/src/SmartSoftware.Identity.HttpApi.Client",
    "modules/identity/src/SmartSoftware.Identity.HttpApi",
    "modules/identity/src/SmartSoftware.Identity.MongoDB",
    "modules/identity/src/SmartSoftware.Identity.Web",
    "modules/identity/src/SmartSoftware.Identity.Blazor",
    "modules/identity/src/SmartSoftware.Identity.Blazor.Server",
    "modules/identity/src/SmartSoftware.Identity.Blazor.WebAssembly",
    "modules/identity/src/SmartSoftware.PermissionManagement.Domain.Identity",
    "modules/identity/src/SmartSoftware.Identity.Installer",
    "source-code/SmartSoftware.Identity.SourceCode",
    
    # modules/identityserver
    "modules/identityserver/src/SmartSoftware.IdentityServer.Domain",
    "modules/identityserver/src/SmartSoftware.IdentityServer.Domain.Shared",
    "modules/identityserver/src/SmartSoftware.IdentityServer.EntityFrameworkCore",
    "modules/identityserver/src/SmartSoftware.IdentityServer.MongoDB",
    "modules/identityserver/src/SmartSoftware.PermissionManagement.Domain.IdentityServer",
    "modules/identityserver/src/SmartSoftware.IdentityServer.Installer",
    "source-code/SmartSoftware.IdentityServer.SourceCode",

    # modules/openiddict
    "modules/openiddict/src/SmartSoftware.OpenIddict.AspNetCore",
    "modules/openiddict/src/SmartSoftware.OpenIddict.Domain",
    "modules/openiddict/src/SmartSoftware.OpenIddict.Domain.Shared",
    "modules/openiddict/src/SmartSoftware.OpenIddict.EntityFrameworkCore",
    "modules/openiddict/src/SmartSoftware.OpenIddict.MongoDB",
    "modules/openiddict/src/SmartSoftware.PermissionManagement.Domain.OpenIddict",
    "modules/openiddict/src/SmartSoftware.OpenIddict.Installer",
    "source-code/SmartSoftware.OpenIddict.SourceCode",

    # modules/permission-management
    "modules/permission-management/src/SmartSoftware.PermissionManagement.Application.Contracts",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.Application",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.Domain",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.Domain.Shared",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.EntityFrameworkCore",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.HttpApi.Client",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.HttpApi",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.MongoDB",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.Web",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.Blazor",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.Blazor.Server",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.Blazor.WebAssembly",
    "modules/permission-management/src/SmartSoftware.PermissionManagement.Installer",
    "source-code/SmartSoftware.PermissionManagement.SourceCode",

    # modules/setting-management
    "modules/setting-management/src/SmartSoftware.SettingManagement.Application.Contracts",
    "modules/setting-management/src/SmartSoftware.SettingManagement.Application",
    "modules/setting-management/src/SmartSoftware.SettingManagement.Blazor",
    "modules/setting-management/src/SmartSoftware.SettingManagement.Blazor.Server",
    "modules/setting-management/src/SmartSoftware.SettingManagement.Blazor.WebAssembly",
    "modules/setting-management/src/SmartSoftware.SettingManagement.Domain",
    "modules/setting-management/src/SmartSoftware.SettingManagement.Domain.Shared",
    "modules/setting-management/src/SmartSoftware.SettingManagement.EntityFrameworkCore",
    "modules/setting-management/src/SmartSoftware.SettingManagement.HttpApi.Client",
    "modules/setting-management/src/SmartSoftware.SettingManagement.HttpApi",
    "modules/setting-management/src/SmartSoftware.SettingManagement.MongoDB",
    "modules/setting-management/src/SmartSoftware.SettingManagement.Web",
    "modules/setting-management/src/SmartSoftware.SettingManagement.Installer",
    "source-code/SmartSoftware.SettingManagement.SourceCode",

    # modules/tenant-management
    "modules/tenant-management/src/SmartSoftware.TenantManagement.Application.Contracts",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.Application",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.Blazor",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.Blazor.Server",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.Blazor.WebAssembly",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.Domain",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.Domain.Shared",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.EntityFrameworkCore",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.HttpApi.Client",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.HttpApi",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.MongoDB",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.Web",
    "modules/tenant-management/src/SmartSoftware.TenantManagement.Installer",
    "source-code/SmartSoftware.TenantManagement.SourceCode",

    # modules/users
    "modules/users/src/SmartSoftware.Users.Abstractions",
    "modules/users/src/SmartSoftware.Users.Domain",
    "modules/users/src/SmartSoftware.Users.Domain.Shared",
    "modules/users/src/SmartSoftware.Users.EntityFrameworkCore",
    "modules/users/src/SmartSoftware.Users.MongoDB",
    "modules/users/src/SmartSoftware.Users.Installer",
    "source-code/SmartSoftware.Users.SourceCode",

    # modules/virtual-file-explorer
    "modules/virtual-file-explorer/src/SmartSoftware.VirtualFileExplorer.Web",
    "modules/virtual-file-explorer/src/SmartSoftware.VirtualFileExplorer.Installer",
    "source-code/SmartSoftware.VirtualFileExplorer.SourceCode",
	
    # modules/blob-storing-database
    "modules/blob-storing-database/src/SmartSoftware.BlobStoring.Database.Domain",
    "modules/blob-storing-database/src/SmartSoftware.BlobStoring.Database.Domain.Shared",
    "modules/blob-storing-database/src/SmartSoftware.BlobStoring.Database.EntityFrameworkCore",
    "modules/blob-storing-database/src/SmartSoftware.BlobStoring.Database.MongoDB",
    "modules/blob-storing-database/src/SmartSoftware.BlobStoring.Database.Installer",
    "source-code/SmartSoftware.BlobStoring.Database.SourceCode",
	
    # smartsoftware/cms-kit	
    "modules/cms-kit/src/SmartSoftware.CmsKit.Admin.Application",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Admin.Application.Contracts",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Admin.HttpApi",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Admin.HttpApi.Client",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Admin.Web",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Application",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Application.Contracts",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Common.Application",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Common.Application.Contracts",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Common.HttpApi",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Common.HttpApi.Client",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Common.Web",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Domain",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Domain.Shared",
    "modules/cms-kit/src/SmartSoftware.CmsKit.EntityFrameworkCore",
    "modules/cms-kit/src/SmartSoftware.CmsKit.HttpApi",
    "modules/cms-kit/src/SmartSoftware.CmsKit.HttpApi.Client",
    "modules/cms-kit/src/SmartSoftware.CmsKit.MongoDB",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Public.Application",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Public.Application.Contracts",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Public.HttpApi",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Public.HttpApi.Client",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Public.Web",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Web",
    "modules/cms-kit/src/SmartSoftware.CmsKit.Installer",
    "source-code/SmartSoftware.CmsKit.SourceCode"
)
