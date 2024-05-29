using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartSoftware.Auditing;
using SmartSoftware.EntityFrameworkCore.ChangeTrackers;

namespace SmartSoftware.EntityFrameworkCore.EntityHistory;

public interface IEntityHistoryHelper
{
    void InitializeNavigationHelper(SmartSoftwareEfCoreNavigationHelper ssEfCoreNavigationHelper);

    List<EntityChangeInfo> CreateChangeList(ICollection<EntityEntry> entityEntries);

    void UpdateChangeList(List<EntityChangeInfo> entityChanges);
}
