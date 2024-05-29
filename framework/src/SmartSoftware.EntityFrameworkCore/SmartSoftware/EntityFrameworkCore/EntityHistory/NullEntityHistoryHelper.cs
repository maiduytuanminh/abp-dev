using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartSoftware.Auditing;
using SmartSoftware.EntityFrameworkCore.ChangeTrackers;

namespace SmartSoftware.EntityFrameworkCore.EntityHistory;

public class NullEntityHistoryHelper : IEntityHistoryHelper
{
    public static NullEntityHistoryHelper Instance { get; } = new NullEntityHistoryHelper();

    private NullEntityHistoryHelper()
    {

    }

    public void InitializeNavigationHelper(SmartSoftwareEfCoreNavigationHelper ssEfCoreNavigationHelper)
    {

    }

    public List<EntityChangeInfo> CreateChangeList(ICollection<EntityEntry> entityEntries)
    {
        return new List<EntityChangeInfo>();
    }

    public void UpdateChangeList(List<EntityChangeInfo> entityChanges)
    {

    }
}
