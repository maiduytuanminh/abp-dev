﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.MongoDB;
using SmartSoftware.Users.MongoDB;
using SmartSoftware.Blogging.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace SmartSoftware.Blogging.Users
{
    public class MongoBlogUserRepository : MongoUserRepositoryBase<IBloggingMongoDbContext, BlogUser>, IBlogUserRepository
    {
        public MongoBlogUserRepository(IMongoDbContextProvider<IBloggingMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<List<BlogUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default)
        {
            var query = await GetMongoQueryableAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(x => x.UserName.Contains(filter));
            }

            return await query.Take(maxCount).ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
