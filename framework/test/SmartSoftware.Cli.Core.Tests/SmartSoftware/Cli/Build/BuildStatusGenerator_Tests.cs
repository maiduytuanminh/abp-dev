using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;

namespace SmartSoftware.Cli.Build;

public class BuildStatusGenerator_Tests : SmartSoftwareCliTestBase
{
    private readonly IBuildStatusGenerator _buildStatusGenerator;
    private IGitRepositoryHelper _gitRepositoryHelper;

    public BuildStatusGenerator_Tests()
    {
        _buildStatusGenerator = GetRequiredService<IBuildStatusGenerator>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _gitRepositoryHelper = Substitute.For<IGitRepositoryHelper>();
        services.AddTransient(provider => _gitRepositoryHelper);
    }

    [Fact]
    public void Should_Set_Repository_CommitId_When_All_Projects_Built()
    {
        var buildConfig = new DotNetProjectBuildConfig
        {
            GitRepository = new GitRepository("smartsoftware", "dev", "")
            {
                DependingRepositories = new List<GitRepository>()
                    {
                        new GitRepository("ss", "dev", "")
                    }
            }
        };

        var changedProjects = new List<DotNetProjectInfo>()
            {
                new DotNetProjectInfo("smartsoftware", "project1.csproj", true)
            };

        var builtProjects = new List<string>()
            {
                "project1.csproj"
            };

        var lastCommitId = "1";
        _gitRepositoryHelper.GetLastCommitId(buildConfig.GitRepository).Returns(lastCommitId);
        _gitRepositoryHelper.GetFriendlyName(buildConfig.GitRepository).Returns("smartsoftware");

        var status = _buildStatusGenerator.Generate(buildConfig, changedProjects, builtProjects);
        status.CommitId.ShouldBe(lastCommitId);
    }


    [Fact]
    public void Should_Set_Repository_CommitId_When_All_Projects_Built_For_Child_Repository()
    {
        var buildConfig = new DotNetProjectBuildConfig
        {
            GitRepository = new GitRepository("smartsoftware", "dev", "")
            {
                DependingRepositories = new List<GitRepository>()
                    {
                        new GitRepository("ss", "dev", "")
                    }
            }
        };

        var changedProjects = new List<DotNetProjectInfo>()
            {
                new DotNetProjectInfo("ss", "project1.csproj", true)
            };

        var builtProjects = new List<string>()
            {
                "project1.csproj"
            };

        var lastCommitId = "1";
        _gitRepositoryHelper.GetLastCommitId(buildConfig.GitRepository).Returns(lastCommitId);
        _gitRepositoryHelper.GetFriendlyName(buildConfig.GitRepository).Returns("ss");

        var status = _buildStatusGenerator.Generate(buildConfig, changedProjects, builtProjects);
        status.CommitId.ShouldBe(lastCommitId);
    }

    [Fact]
    public void Should_Not_Set_Repository_CommitId_When_Building_Single_Solution()
    {
        var buildConfig = new DotNetProjectBuildConfig
        {
            GitRepository = new GitRepository("smartsoftware", "dev", "")
            {
                DependingRepositories = new List<GitRepository>()
                    {
                        new GitRepository("ss", "dev", "")
                    }
            },
            SlFilePath = "test.sln"
        };

        var changedProjects = new List<DotNetProjectInfo>()
            {
                new DotNetProjectInfo("smartsoftware", "project1.csproj", true)
            };

        var builtProjects = new List<string>()
            {
                "project1.csproj"
            };

        var lastCommitId = "1";
        _gitRepositoryHelper.GetLastCommitId(buildConfig.GitRepository).Returns(lastCommitId);
        _gitRepositoryHelper.GetFriendlyName(buildConfig.GitRepository).Returns("smartsoftware");

        var status = _buildStatusGenerator.Generate(buildConfig, changedProjects, builtProjects);
        status.CommitId.ShouldBeNull();
    }
}
