﻿using JetBrains.Annotations;
using SmartSoftware;
using SmartSoftware.Application.Dtos;

namespace System.Linq;

public static class SmartSoftwarePagingQueryableExtensions
{
    /// <summary>
    /// Used for paging with an <see cref="IPagedResultRequest"/> object.
    /// </summary>
    /// <param name="query">Queryable to apply paging</param>
    /// <param name="pagedResultRequest">An object implements <see cref="IPagedResultRequest"/> interface</param>
    public static IQueryable<T> PageBy<T>([NotNull] this IQueryable<T> query, IPagedResultRequest pagedResultRequest)
    {
        Check.NotNull(query, nameof(query));

        return query.PageBy(pagedResultRequest.SkipCount, pagedResultRequest.MaxResultCount);
    }
}
