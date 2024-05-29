﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware.Domain.Repositories;

namespace SmartSoftware.CmsKit.Comments;

public interface ICommentRepository : IBasicRepository<Comment, Guid>
{
    Task<CommentWithAuthorQueryResultItem> GetWithAuthorAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<CommentWithAuthorQueryResultItem>> GetListAsync(
        string filter = null,
        string entityType = null,
        Guid? repliedCommentId = null,
        string authorUsername = null,
        DateTime? creationStartDate = null,
        DateTime? creationEndDate = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        string text = null,
        string entityType = null,
        Guid? repliedCommentId = null,
        string authorUsername = null,
        DateTime? creationStartDate = null,
        DateTime? creationEndDate = null,
        CancellationToken cancellationToken = default
    );

    Task<List<CommentWithAuthorQueryResultItem>> GetListWithAuthorsAsync(
        [NotNull] string entityType,
        [NotNull] string entityId,
        CancellationToken cancellationToken = default
    );

    Task DeleteWithRepliesAsync(
        Comment comment,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsAsync(string idempotencyToken, CancellationToken cancellationToken = default);
}
