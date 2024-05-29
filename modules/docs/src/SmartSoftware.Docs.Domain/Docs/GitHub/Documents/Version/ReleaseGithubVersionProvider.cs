using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Internal;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Docs.GitHub.Documents.Version
{
    public class ReleaseGithubVersionProvider : IGithubVersionProvider, ITransientDependency
    {
        public async Task<List<GithubVersion>> GetVersions(string name, string repositoryName, string token)
        {
            var client = GetGitHubClient(name, token);
            var releases = await client.Repository.Release.GetAll(name, repositoryName);

            return releases.Select(r => new GithubVersion {Name = r.TagName}).ToList();
        }

        private static GitHubClient GetGitHubClient(string name, string token)
        {
            return token.IsNullOrWhiteSpace()
                ? new GitHubClient(new ProductHeaderValue(name))
                : new GitHubClient(new ProductHeaderValue(name), new InMemoryCredentialStore(new Credentials(token)));
        }
    }
}
