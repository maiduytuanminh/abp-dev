﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories;

namespace SmartSoftware.Blogging.Comments
{
    public interface ICommentRepository : IBasicRepository<Comment, Guid>
    {
        Task<List<Comment>> GetListOfPostAsync(Guid postId, CancellationToken cancellationToken = default);

        Task<int> GetCommentCountOfPostAsync(Guid postId, CancellationToken cancellationToken = default);

        Task<List<Comment>> GetRepliesOfComment(Guid id, CancellationToken cancellationToken = default);

        Task DeleteOfPost(Guid id, CancellationToken cancellationToken = default);
    }
}
