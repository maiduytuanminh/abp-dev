using System;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.ObjectMapping;

namespace SmartSoftware.Domain.Entities.Events.Distributed.EntitySynchronizers.WithEntityVersion;

public class BookSynchronizer : EntitySynchronizer<Book, Guid, RemoteBookEto>, ITransientDependency
{
    public BookSynchronizer(IObjectMapper objectMapper, IRepository<Book, Guid> repository)
        : base(objectMapper, repository)
    {
    }
}