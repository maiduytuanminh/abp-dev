using Shouldly;
using SmartSoftware.Cli.ProjectModification;
using Xunit;

namespace SmartSoftware.Cli;

public class ProjectVersionParse_Tests
{
    [Fact]
    public void Find_SmartSoftware_Version()
    {
        const string csprojContent = "<Project Sdk=\"Microsoft.NET.Sdk\">" +
                                     "<Import Project=\"..\\..\\common.props\" />" +
                                     "<PropertyGroup>" +
                                     "<TargetFramework>net5.0</TargetFramework>" +
                                     "<RootNamespace>Blazoor.EfCore07062034</RootNamespace>" +
                                     "</PropertyGroup>" +
                                     "<ItemGroup>" +
                                     "<ProjectReference Include=\"..\\Blazoor.EfCore07062034.Domain.Shared\\Blazoor.EfCore07062034.Domain.Shared.csproj\" />" +
                                     "</ItemGroup>" +
                                     "<ItemGroup>" +
                                     "<PackageReference    Include=\"SmartSoftware.Emailing\"   Version=\"4.4.0-rc.1\"  />" +
                                     "<PackageReference  Include=\"SmartSoftware.PermissionManagement.Domain.Identity\"   Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.IdentityServer.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.PermissionManagement.Domain.IdentityServer\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.BackgroundJobs.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.AuditLogging.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.FeatureManagement.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.SettingManagement.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.BlobStoring.Database.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.Identity.Pro.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.LanguageManagement.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.LeptonTheme.Management.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.Saas.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"SmartSoftware.TextTemplateManagement.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "</ItemGroup>" +
                                     "</Project>";

        var success = SolutionPackageVersionFinder.TryParseVersionFromCsprojFile(csprojContent, out var version);
        success.ShouldBe(true);
        version.ShouldBe("4.4.0-rc.1");
    }

    [Fact]
    public void Find_SmartSoftware_Semantic_Version()
    {
        const string csprojContent = "<Project Sdk=\"Microsoft.NET.Sdk\">" +
                                     "<Import Project=\"..\\..\\common.props\" />" +
                                     "<PropertyGroup>" +
                                     "<TargetFramework>net5.0</TargetFramework>" +
                                     "<RootNamespace>Blazoor.EfCore07062034</RootNamespace>" +
                                     "</PropertyGroup>" +
                                     "<ItemGroup>" +
                                     "<ProjectReference Include=\"..\\Blazoor.EfCore07062034.Domain.Shared\\Blazoor.EfCore07062034.Domain.Shared.csproj\" />" +
                                     "</ItemGroup>" +
                                     "<ItemGroup>" +
                                     "<PackageReference Include=\"SmartSoftware.Emailing\" Version=\"12.8.3-beta.1\"  />" +
                                     "</ItemGroup>" +
                                     "</Project>";

        var success = SolutionPackageVersionFinder.TryParseSemanticVersionFromCsprojFile(csprojContent, out var version);
        success.ShouldBe(true);
        version.Major.ShouldBe(12);
        version.Minor.ShouldBe(8);
        version.Patch.ShouldBe(3);
        version.Release.ShouldBe("beta.1");
    }

}
