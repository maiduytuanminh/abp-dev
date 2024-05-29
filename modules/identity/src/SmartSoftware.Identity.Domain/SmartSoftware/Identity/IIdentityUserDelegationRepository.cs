﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories;

namespace SmartSoftware.Identity;

public interface IIdentityUserDelegationRepository: IBasicRepository<IdentityUserDelegation, Guid>
{
    Task<List<IdentityUserDelegation>> GetListAsync(
        Guid? sourceUserId,
        Guid? targetUserId,
        CancellationToken cancellationToken = default);

    Task<List<IdentityUserDelegation>> GetActiveDelegationsAsync(
        Guid targetUserId,
        CancellationToken cancellationToken = default);

    Task<IdentityUserDelegation> FindActiveDelegationByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
