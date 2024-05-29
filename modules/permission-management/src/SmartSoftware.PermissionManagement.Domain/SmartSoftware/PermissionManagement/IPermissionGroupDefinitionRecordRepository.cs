using System;
using SmartSoftware.Domain.Repositories;

namespace SmartSoftware.PermissionManagement;

public interface IPermissionGroupDefinitionRecordRepository : IBasicRepository<PermissionGroupDefinitionRecord, Guid>
{
    
}