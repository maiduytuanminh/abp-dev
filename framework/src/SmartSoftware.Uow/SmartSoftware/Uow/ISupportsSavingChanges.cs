﻿using System.Threading;
using System.Threading.Tasks;

namespace SmartSoftware.Uow;

public interface ISupportsSavingChanges
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
