using System;
using System.Collections.Generic;
using System.Text;
using SmartSoftware.Domain.Services;

namespace SmartSoftware.Docs.GitHub.Documents
{
    public interface IGithubPatchAnalyzer : IDomainService
    {
        bool HasPatchSignificantChanges(string patch);
    }
}
