using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware.Domain.Repositories;

namespace SmartSoftware.CmsKit.GlobalResources;

public interface IGlobalResourceRepository: IBasicRepository<GlobalResource, Guid>
{
    Task<GlobalResource> FindByNameAsync([NotNull] string name, CancellationToken cancellationToken = default);
}