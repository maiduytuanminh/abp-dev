namespace SmartSoftware.Cli.Build;

public interface IGitRepositoryHelper
{
    string GetLastCommitId(GitRepository repository);

    string GetFriendlyName(GitRepository repository);
}
