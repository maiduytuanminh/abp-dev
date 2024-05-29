using System;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.CmsKit.EntityFrameworkCore;

namespace SmartSoftware.CmsKit.MediaDescriptors;

public class EfCoreMediaDescriptorRepository : EfCoreRepository<ICmsKitDbContext, MediaDescriptor, Guid>, IMediaDescriptorRepository
{
    public EfCoreMediaDescriptorRepository(IDbContextProvider<ICmsKitDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
