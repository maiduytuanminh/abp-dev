﻿using System;
using System.Threading.Tasks;

namespace SmartSoftware.Identity;

public interface IUserRoleFinder
{
    [Obsolete("Use GetRoleNamesAsync instead.")]
    Task<string[]> GetRolesAsync(Guid userId);
    
    Task<string[]> GetRoleNamesAsync(Guid userId);
}
