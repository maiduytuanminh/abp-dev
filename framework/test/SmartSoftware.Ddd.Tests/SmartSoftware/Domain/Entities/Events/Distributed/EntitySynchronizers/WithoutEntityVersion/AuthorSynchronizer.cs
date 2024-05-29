using System;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.ObjectMapping;

namespace SmartSoftware.Domain.Entities.Events.Distributed.EntitySynchronizers.WithoutEntityVersion;

public class AuthorSynchronizer : EntitySynchronizer<Author, Guid, RemoteAuthorEto>, ITransientDependency
{
    public AuthorSynchronizer(IObjectMapper objectMapper, IRepository<Author, Guid> repository)
        : base(objectMapper, repository)
    {
    }
}