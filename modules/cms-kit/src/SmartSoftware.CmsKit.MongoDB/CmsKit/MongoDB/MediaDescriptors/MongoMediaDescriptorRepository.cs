using System;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.MongoDB;
using SmartSoftware.CmsKit.MediaDescriptors;

namespace SmartSoftware.CmsKit.MongoDB.MediaDescriptors;

public class MongoMediaDescriptorRepository : MongoDbRepository<ICmsKitMongoDbContext, MediaDescriptor, Guid>, IMediaDescriptorRepository
{
    public MongoMediaDescriptorRepository(IMongoDbContextProvider<ICmsKitMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
